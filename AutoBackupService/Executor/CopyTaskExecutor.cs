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

            WriteLog(taskVO.SourcePath, "Start copy task [" + taskVO.TaskName + "].");
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
                        WriteLog(taskVO.SourcePath, "Check file [" + file.FullName + "].");
                        string targetFileName = file.FullName.Replace(taskVO.SourcePath, "");
                        FileInfo[] targetFiles = targetDir.GetFiles(targetFileName, so);
                        if (targetFiles != null && targetFiles.Length > 0)
                        {
                            WriteLog(taskVO.SourcePath, "Find target file [" + targetFiles[0].FullName + "].");
                            string sourceFileHash = GetHash(file.FullName);
                            string targetFileHash = GetHash(targetFiles[0].FullName);

                            if (!sourceFileHash.Equals(targetFileHash))
                            {
                                WriteLog(taskVO.SourcePath, "File [" + targetFiles[0].FullName + "] is not same with the source [" + file.FullName + "].");
                                DeleteFile(targetFiles[0]);
                                WriteLog(taskVO.SourcePath, "File [" + targetFiles[0].FullName + "] removed.");
                            }
                        }

                        string targetFullFileName = Path.Combine(new string[] { targetDir.FullName, targetFileName });
                        CopyFile(file.FullName, targetFullFileName);
                        WriteLog(taskVO.SourcePath, "File [" + file.FullName + "] has been copied to [" + targetFullFileName + "]");
                    }
                }
            }
        }
    }
}
