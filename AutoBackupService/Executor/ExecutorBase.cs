using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService.Executor
{
    public abstract class ExecutorBase
    {
        public TaskBaseVO TaskVO { get; set; }

        public abstract void Execute();

        /// <summary>
        /// 寫log
        /// </summary>
        /// <param name="filePath">要寫入的Log資料夾</param>
        /// <param name="logMsg">要寫入的Log資訊</param>
        protected static void WriteLog(String filePath, String logMsg)
        {
            String logFileName = DateTime.Now.Year.ToString() + int.Parse(DateTime.Now.Month.ToString()).ToString("00") + int.Parse(DateTime.Now.Day.ToString()).ToString("00") + ".log";
            String nowTime = int.Parse(DateTime.Now.Hour.ToString()).ToString("00") + ":" + int.Parse(DateTime.Now.Minute.ToString()).ToString("00") + ":" + int.Parse(DateTime.Now.Second.ToString()).ToString("00");

            if (!Directory.Exists(filePath))
            {
                //建立資料夾
                Directory.CreateDirectory(filePath);
            }

            if (!File.Exists(logFileName))
            {
                //建立檔案
                File.Create(filePath + "\\" + logFileName);
            }

            using (StreamWriter sw = File.AppendText(filePath + "\\" + logFileName))
            {
                sw.WriteLine("-- Execute Time " + nowTime + " --");
                sw.WriteLine(logMsg);
                sw.WriteLine();
            }
        }

        protected static string GetHash(string path)
        {
            var hash = SHA1.Create();
            var stream = new FileStream(path, FileMode.Open);
            byte[] hashByte = hash.ComputeHash(stream);
            stream.Close();
            return BitConverter.ToString(hashByte).Replace("-", "");
        }

        protected static void DeleteFile(FileInfo fileInfo)
        {
            File.Delete(fileInfo.FullName);
        }

        protected static void CopyFile(string sourceFullFileName, string targetFullFileName)
        {
            File.Copy(sourceFullFileName, targetFullFileName);
        }
    }
}
