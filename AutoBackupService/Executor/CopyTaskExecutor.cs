using System;
using System.Collections.Generic;
using System.IO;

namespace AutoBackupService.Executor
{
    public class CopyTaskExecutor : ExecutorBase
    {
        public override void AfterExecute()
        {
            TaskVO.LastRunTerm = DateTime.Now.Subtract(TaskVO.LastRunTime).Milliseconds;
            TaskVO.SessionRunTimes++;
            Logger.WriteLog("TASK", "Copy task [" + TaskVO.TaskName + "] finished.");
            Logger.WriteLog("TASK", "Copy task [" + TaskVO.TaskName + "] spent " + TaskVO.LastRunTerm + "(ms).");
            TaskVO.NextRunTime = DateTime.Now.AddMilliseconds(TaskVO.Interval);
            Logger.WriteLog("TASK", "Copy task [" + TaskVO.TaskName + "] will run at " + TaskVO.NextRunTime + " next time.");
        }

        public override void BeforeExecute()
        {
            if (TaskVO == null)
            {
                throw new Exception("任務不能為空");
            }

            if (!(TaskVO is CopyTaskVO))
            {
                throw new Exception("任務類型錯誤");
            }

            TaskVO.LastRunTime = DateTime.Now;
            Logger.WriteLog("TASK", "Start copy task [" + TaskVO.TaskName + "].");
        }

        public override void DoExecute()
        {
            CopyTaskVO taskVO = (CopyTaskVO)TaskVO;

            if (!taskVO.Method.Equals(CopyTaskVO.TaskMethodEnum.Current))
            {
                CopyDir(taskVO.SourcePath, taskVO.TargetPath, taskVO.DirPattern, taskVO.FilePattern);
            }

            CopyFile(taskVO.SourcePath, taskVO.TargetPath, taskVO.FilePattern);
        }

        private void CopyDir(string sourcePath, string targetPath, string[] dirPatterns, string[] filePatterns)
        {
            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
            foreach (string dirPattern in dirPatterns)
            {
                DirectoryInfo[] dirs = sourceDir.GetDirectories("*", SearchOption.TopDirectoryOnly);
                if (dirs != null && dirs.Length > 0)
                {
                    foreach(DirectoryInfo dir in dirs)
                    {
                        string targetDirName = dir.FullName.Replace(sourceDir.FullName, targetPath);
                        if (!Directory.Exists(targetDirName))
                        {
                            Directory.CreateDirectory(targetDirName);
                            CopyFile(dir.FullName, targetDirName, filePatterns);
                        }

                        CopyDir(dir.FullName, targetDirName, dirPatterns, filePatterns);
                    }
                }
            }
        }

        private static void CopyFile(string sourcePath, string targetPath, string[] filePatterns)
        {
            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
            foreach (string filePattern in filePatterns)
            {
                FileInfo[] files = sourceDir.GetFiles(filePattern, SearchOption.TopDirectoryOnly);
                if (files != null && files.Length > 0)
                {
                    DoCopyFiles(sourcePath, targetPath, files);
                }
            }
        }

        private static void DoCopyFiles(string sourcePath, string targetPath, FileInfo[] files)
        {
            DirectoryInfo targetDir = new DirectoryInfo(targetPath);
            foreach (FileInfo file in files)
            {
                //Logger.WriteLog("TASK", "Check source file [" + file.FullName + "].");
                string targetFileName = file.FullName.Replace(sourcePath, targetPath);
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

                        if (sourceFileHash.Equals(targetFileHash))
                        {
                            continue;
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
                File.Copy(file.FullName, targetFullFileName, true);
                Logger.WriteLog("TASK", "File [" + file.FullName + "] copied to [" + targetFullFileName + "]");
            }
        }
    }
}
