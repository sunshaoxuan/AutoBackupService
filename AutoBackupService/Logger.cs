using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService
{
    public class Logger
    {

        /// <summary>
        /// 错误信息
        /// </summary>
        private static string ErrorInfo { get; set; }

        public static void WriteLog(string title, string logMsg)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string logFileName = "ABS_LOG_" + DateTime.Now.Year.ToString() + int.Parse(DateTime.Now.Month.ToString()).ToString("00") + int.Parse(DateTime.Now.Day.ToString()).ToString("00") + ".log";

            if (!Directory.Exists(filePath))
            {
                //建立資料夾
                Directory.CreateDirectory(filePath);
            }

            try
            {
                using (var stream = new StreamWriter(System.IO.Path.Combine(filePath, logFileName), true))
                {
                    stream.WriteLine("$$time={0:yyyy-MM-dd HH:mm:ss} $$msg=Info{1}", DateTime.Now, "["+ title + "]" + logMsg);
                }
            }
            catch (Exception ex)
            {
                CreateSystemEventLogCategory("AutoBackupService", ex.Message);
            }
        }

        /// <summary>
        /// 寫log
        /// </summary>
        /// <param name="filePath">要寫入的Log資料夾</param>
        /// <param name="logMsg">要寫入的Log資訊</param>
        public static void WriteLog(string logMsg) 
        {
            WriteLog("", logMsg);
        }


        /// <summary>
        /// 创建系统事件日志分类
        /// </summary>
        /// <param name="eventSourceName">注册事件源(比如说这个日志来源于某一个应用程序)</param>
        /// <param name="logName">日志名称(事件列表显示的名称)</param>
        /// <returns></returns>
        public static bool CreateSystemEventLogCategory(string eventSourceName, string logName)
        {
            bool createResult = false;
            try
            {
                if (!EventLog.SourceExists(eventSourceName))
                {
                    EventLog.CreateEventSource(eventSourceName, logName);
                }
                createResult = true;
            }
            catch (Exception ex)
            {
                createResult = false;
                ErrorInfo = ex.Message;
            }

            return createResult;
        }
     }
}
