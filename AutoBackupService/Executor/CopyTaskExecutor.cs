using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService.Executor
{
    public class CopyTaskExecutor : ExecutorBase
    {
        public override void Execute()
        {
            if (TaskVO == null)
            {
                throw new Exception("任務不能為空");
            }

            CopyTaskVO taskVO = null;
            if (!(TaskVO is CopyTaskVO))
            {
                throw new Exception("任務類型錯誤");
            }
            else
            {
                taskVO = (CopyTaskVO)TaskVO;
            }

            Logger.WriteLog("TASK", "Start copy task [" + taskVO.TaskName + "].");
            //取得所有文件列表
            List<String> fileList = new List<string>();
            SearchOption so = taskVO.Method.Equals(CopyTaskVO.TaskMethodEnum.Current) ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories;
            foreach (string filter in taskVO.SourceFileFilters)
            {
                DirectoryInfo sourceDir = new DirectoryInfo(taskVO.SourcePath);
                FileInfo[] files = null;
                files = sourceDir.GetFiles(filter, so);

                if (files != null && files.Length > 0)
                {
                    DirectoryInfo targetDir = new DirectoryInfo(taskVO.TargetPath);
                    foreach (FileInfo file in files)
                    {
                        //Logger.WriteLog("TASK", "Check source file [" + file.FullName + "].");
                        string targetFileName = file.FullName.Replace(taskVO.SourcePath, taskVO.TargetPath);
                        //Logger.WriteLog("TASK", "Target file name [" + targetFileName + "].");

                        try
                        {
                            if (File.Exists(targetFileName))
                            {
                                //Logger.WriteLog("TASK", "Found target file [" + targetFileName + "].");
                                string sourceFileHash = GetHash(file.FullName);
                                //Logger.WriteLog("TASK", "Source file hash code: [" + sourceFileHash + "].");
                                string targetFileHash = GetHash(targetFileName);
                                //Logger.WriteLog("TASK", "Target file hash code: [" + targetFileHash + "].");

                                if (!sourceFileHash.Equals(targetFileHash))
                                {
                                    //Logger.WriteLog("TASK", "File [" + targetFileName + "] is not same with the source [" + file.FullName + "].");
                                    File.Delete(targetFileName);
                                    Logger.WriteLog("File [" + targetFileName + "] removed.");
                                }
                            }
                            else
                            {
                                //Logger.WriteLog("TASK", "Target file was not found.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLog("TASK", ex.Message);
                        }

                        string targetFullFileName = Path.Combine(new string[] { targetDir.FullName, targetFileName });
                        File.Copy(file.FullName, targetFullFileName);
                        Logger.WriteLog("TASK", "File [" + file.FullName + "] copied to [" + targetFullFileName + "]");
                    }
                }
            }
        }
    }
}
