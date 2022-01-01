using AutoBackupService.Task;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService.Executor
{
    internal class ZipTaskExecutor : ExecutorBase
    {
        public override void AfterExecute()
        {
            TaskVO.LastRunTerm = DateTime.Now.Subtract(TaskVO.LastRunTime).Milliseconds;
            TaskVO.SessionRunTimes++;
            Logger.WriteLog("TASK", "Zip task [" + TaskVO.TaskName + "] finished.");
            Logger.WriteLog("TASK", "Zip task [" + TaskVO.TaskName + "] spent " + TaskVO.LastRunTerm + "(ms).");
            TaskVO.NextRunTime = DateTime.Now.AddMilliseconds(TaskVO.Interval);
            Logger.WriteLog("TASK", "Zip task [" + TaskVO.TaskName + "] will run at " + TaskVO.NextRunTime + " next time.");
        }

        public override void BeforeExecute()
        {
            if (TaskVO == null)
            {
                throw new Exception("任務不能為空");
            }

            if (!(TaskVO is ZipTaskVO))
            {
                throw new Exception("任務類型錯誤");
            }

            TaskVO.LastRunTime = DateTime.Now;
            Logger.WriteLog("TASK", "Start Zip task [" + TaskVO.TaskName + "].");
        }

        public override void DoExecute()
        {
            ZipTaskVO taskVO = (ZipTaskVO)TaskVO;
            Dictionary<string, string> sourceHash = GetSourcePathHash(taskVO.SourceFileFullPaths);
            if (FileHashRecordsNotChanged(sourceHash))
            {
                //Hash没变，检查Zip文件是否存在
                if (!File.Exists(taskVO.TargetZipFileFullPath))
                {
                    //Zip文件不存在，则可能是以下两种情况造成
                    // 1. 其他线程更新过Hash，但当前线程未执行过
                    // 2. Zip文件已被删除
                    CreateZipFile(taskVO.SourceFileFullPaths, taskVO.TargetZipFileFullPath);
                }
                else
                {
                    //Zip已存在，检查任务Zip文件Hash是否变化了，没变化就跳过
                    if (!FileHashRecordsNotChanged(
                            new Dictionary<string, string>() {
                                { taskVO.TargetZipFileFullPath, 
                                  PublicUtils.GetFileHash(taskVO.TargetZipFileFullPath) }
                            }
                        ))
                    {
                        //变化了
                        CreateZipFile(taskVO.SourceFileFullPaths, taskVO.TargetZipFileFullPath);
                    }
                }
            }
        }

        private void CreateZipFile(string[] sourceFileFullPaths, string targetZipFileFullPath)
        {
            //判断文件是否存在
            Boolean targetFileExists = File.Exists(targetZipFileFullPath);

            if (targetFileExists)
            {
                //如果存在就删除
                File.Delete(targetZipFileFullPath);
                targetFileExists = false;
            }

            foreach (string sourcePath in sourceFileFullPaths)
            {
                if (!targetFileExists)
                {
                    if (Directory.Exists(sourcePath))
                    {
                        //路径如果是文件夹，就压缩整个文件夹
                        ZipFile.CreateFromDirectory(sourcePath, targetZipFileFullPath, CompressionLevel.Optimal, false, Encoding.UTF8);
                        targetFileExists = true;
                    }
                    else if (File.Exists(sourcePath))
                    {
                        //路径如果是文件，就压缩文件
                        using (FileStream fileToWrite = new FileStream(targetZipFileFullPath, FileMode.Open))
                        {
                            ZipArchive zipFile = new ZipArchive(fileToWrite, ZipArchiveMode.Create, false, Encoding.UTF8);

                            string realName = new FileInfo(targetZipFileFullPath).Name;
                            zipFile.CreateEntryFromFile(targetZipFileFullPath, realName, CompressionLevel.Optimal);
                            targetFileExists = true;
                        }
                    }
                }
                else
                {
                    using (FileStream fileToWrite = new FileStream(targetZipFileFullPath, FileMode.Open))
                    {
                        ZipArchive zipFile = new ZipArchive(fileToWrite, ZipArchiveMode.Update, false, Encoding.UTF8);
                        if (Directory.Exists(sourcePath))
                        {
                            //路径如果是文件夹，就压缩整个文件夹
                            string realName = new DirectoryInfo(sourcePath).Name;
                            if (zipFile.GetEntry(realName) == null)
                            {
                                //同名文件夹不存在则可执行压缩
                                AppendDirectoryEntryToZipFile(sourcePath, zipFile, "/");
                            }
                            else
                            {
                                throw new Exception("同名文件夹 [" + realName + "] 在压缩文件中已经存在，无法再次创建。");
                            }
                        }
                        else if (File.Exists(sourcePath))
                        {
                            //路径如果是文件，就压缩文件
                            AppendFileEntryToZipFile(sourcePath, zipFile, "/");
                        }
                    }
                }
            }
        }

        private void AppendDirectoryEntryToZipFile(string sourcePath, ZipArchive zipFile, string rootPath)
        {
            throw new NotImplementedException();
        }

        private void AppendFileEntryToZipFile(string sourcePath, ZipArchive zipFile, string rootPath)
        {
            string fileName = new FileInfo(sourcePath).Name;
            if (rootPath.Equals("/"))
            {
                zipFile.CreateEntryFromFile(sourcePath, fileName, CompressionLevel.Optimal);
            }
        }

        private Dictionary<string, string> GetSourcePathHash(string[] sourceFileFullPaths)
        {
            Dictionary<string, string> sourceHash = new Dictionary<string, string>();
            foreach (string filePath in sourceFileFullPaths)
            {
                if (File.Exists(filePath))
                {
                    //filePath是文件
                    string filePathHash = PublicUtils.GetFileHash(filePath);
                    sourceHash.Add(filePath, filePathHash);
                }
                else if (Directory.Exists(filePath))
                {
                    //filePath是文件夹
                    string dirPathHash = PublicUtils.GetDirectoryHash(filePath);
                    sourceHash.Add(filePath, dirPathHash);
                }
            }

            //检查存档的文件及文件夹Hash
            return sourceHash;
        }

        /// <summary>
        /// 检查文件中存储的HashCode是否无变化
        /// </summary>
        /// <param name="sourceHash"></param>
        /// <returns>True: 无变化   False：有变化</returns>
        /// <exception cref="NotImplementedException"></exception>
        private bool FileHashRecordsNotChanged(Dictionary<string, string> sourceHash)
        {
            throw new NotImplementedException();
        }
    }
}
