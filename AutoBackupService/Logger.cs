using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService
{
    public class Logger
    {
        /// <summary>
        /// 寫log
        /// </summary>
        /// <param name="filePath">要寫入的Log資料夾</param>
        /// <param name="logMsg">要寫入的Log資訊</param>
        public static void WriteLog(string logMsg)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string logFileName = DateTime.Now.Year.ToString() + int.Parse(DateTime.Now.Month.ToString()).ToString("00") + int.Parse(DateTime.Now.Day.ToString()).ToString("00") + ".log";
            string nowTime = int.Parse(DateTime.Now.Hour.ToString()).ToString("00") + ":" + int.Parse(DateTime.Now.Minute.ToString()).ToString("00") + ":" + int.Parse(DateTime.Now.Second.ToString()).ToString("00");

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
    }
}
