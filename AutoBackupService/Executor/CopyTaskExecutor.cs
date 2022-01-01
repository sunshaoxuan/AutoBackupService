using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
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

            if (taskVO.Method.Equals(CopyTaskVO.TaskMethodEnum.Recursion))
            {
                CopyDir(taskVO.SourcePath, taskVO.TargetPath, taskVO.DirPattern, taskVO.DirExclude, taskVO.FilePattern, taskVO.FileExclude);
            }

            CopyFile(taskVO.SourcePath, taskVO.TargetPath, taskVO.FilePattern, taskVO.DirExclude, taskVO.FileExclude);
        }

        private void CopyDir(string sourcePath, string targetPath, string[] dirPatterns, string[] dirExcludes,  string[] filePatterns, string[] fileExlcudes)
        {
            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
            if (KeyContains(dirExcludes, sourceDir.Name))
            {
                return;
            }

            foreach (string dirPattern in dirPatterns)
            {
                DirectoryInfo[] dirs = sourceDir.GetDirectories(dirPattern, SearchOption.TopDirectoryOnly);
                if (dirs != null && dirs.Length > 0)
                {
                    foreach (DirectoryInfo dir in dirs)
                    {
                        if (KeyContains(dirExcludes, dir.Name))
                        {
                            continue;
                        }

                        string targetDirName = dir.FullName.Replace(sourceDir.FullName, targetPath);
                        if (!Directory.Exists(targetDirName))
                        {
                            Directory.CreateDirectory(targetDirName);
                            CopyFile(dir.FullName, targetDirName, filePatterns, dirExcludes, fileExlcudes);
                        }

                        CopyDir(dir.FullName, targetDirName, dirPatterns, dirExcludes, filePatterns, fileExlcudes);
                    }
                }
            }

            CopyFile(sourcePath, targetPath, filePatterns, dirExcludes, fileExlcudes);
        }

        private static bool KeyContains(string[] patterns, string beConpairedString)
        {
            if(patterns == null || patterns.Length == 0)
            {
                return false;
            }
            else
            {
                foreach(string dirEx in patterns)
                {
                    if (Operators.LikeString(beConpairedString.ToUpper(), dirEx.ToUpper(), CompareMethod.Text))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static void CopyFile(string sourcePath, string targetPath, string[] filePatterns, string[] dirExcludes, string[] fileExcludes)
        {
            DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
            if (KeyContains(dirExcludes, sourceDir.Name))
            {
                return;
            }

            foreach (string filePattern in filePatterns)
            {
                FileInfo[] files = sourceDir.GetFiles(filePattern, SearchOption.TopDirectoryOnly);
                if (files != null && files.Length > 0)
                {
                    DoCopyFiles(sourcePath, targetPath, files, fileExcludes);
                }
            }
        }

        private static void DoCopyFiles(string sourcePath, string targetPath, FileInfo[] files, string[] fileExcludes)
        {
            DirectoryInfo targetDir = new DirectoryInfo(targetPath);
            foreach (FileInfo file in files)
            {
                if (KeyContains(fileExcludes, file.Name))
                {
                    continue;
                }
                //Logger.WriteLog("TASK", "Check source file [" + file.FullName + "].");
                string targetFileName = file.FullName.Replace(sourcePath, targetPath);
                //Logger.WriteLog("TASK", "Target file name [" + targetFileName + "].");

                try
                {
                    if (File.Exists(targetFileName))
                    {
                        //Logger.WriteLog("TASK", "Found target file [" + targetFileName + "].");
                        string sourceFileHash = PublicUtils.GetFileHash(file.FullName);
                        //Logger.WriteLog("TASK", "Source file hash code: [" + sourceFileHash + "].");
                        string targetFileHash = PublicUtils.GetFileHash(targetFileName);
                        //Logger.WriteLog("TASK", "Target file hash code: [" + targetFileHash + "].");

                        if (sourceFileHash.Equals(targetFileHash))
                        {
                            continue;
                        }
                        else
                        {
                            Logger.WriteLog("TASK", "Target file [" + targetFileName + "] was modified.");
                        }
                    }
                    else
                    {
                        Logger.WriteLog("TASK", "Target file ["+ targetFileName + "] was not found.");
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
