using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService.Utils
{
    public class CommandRunner
    {
        public string ExecutablePath { get; }
        public string WorkingDirectory { get; }

        public CommandRunner(string executablePath, string workingDirectory = null)
        {
            ExecutablePath = executablePath ?? throw new ArgumentNullException(nameof(executablePath));
            WorkingDirectory = workingDirectory ?? Path.GetDirectoryName(executablePath);
        }

        public string Run(string arguments)
        {
            var info = new ProcessStartInfo(ExecutablePath, arguments)
            {
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                WorkingDirectory = WorkingDirectory,
            };
            Process prc = Process.Start(info);
            prc.WaitForExit();//等待程序执行完退出进程
            string strResult= prc.StandardOutput.ReadToEnd();
            prc.Close();//关闭进程
            return strResult;
        }
    }
}
