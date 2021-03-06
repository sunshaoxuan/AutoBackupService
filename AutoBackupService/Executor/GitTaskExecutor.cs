using AutoBackupService.Executor;
using AutoBackupService.Task;
using AutoBackupService.Utils;
using System;
using System.Diagnostics;
using System.Security.Principal;

namespace AutoBackupService 
{
    internal class GitTaskExecutor : ExecutorBase
    {
        public override void AfterExecute()
        {
            TaskVO.LastRunTerm = DateTime.Now.Subtract(TaskVO.LastRunTime).Milliseconds;
            TaskVO.SessionRunTimes++;
            Logger.WriteLog("TASK", "Git task [" + TaskVO.TaskName + "] finished.");
            Logger.WriteLog("TASK", "Git task [" + TaskVO.TaskName + "] spent " + TaskVO.LastRunTerm + "(ms).");
            TaskVO.NextRunTime = DateTime.Now.AddMilliseconds(TaskVO.Interval);
            Logger.WriteLog("TASK", "Git task [" + TaskVO.TaskName + "] will run at " + TaskVO.NextRunTime + " next time.");
        }

        public override void BeforeExecute()
        {
            if (TaskVO == null)
            {
                throw new Exception("任務不能為空");
            }

            if (!(TaskVO is GitTaskVO))
            {
                throw new Exception("任務類型錯誤");
            }

            TaskVO.LastRunTime = DateTime.Now;
            Logger.WriteLog("TASK", "Start Git task [" + TaskVO.TaskName + "].");
        }

        public override void DoExecute()
        {
            GitTaskVO taskVO = (GitTaskVO)TaskVO;
            CommandRunner git = new CommandRunner(@"gitpull.bat", AppContext.BaseDirectory);

            string info = git.Run(null);
            Logger.WriteLog("TASK", "Git task [" + TaskVO.TaskName + "] information:" + info);
        }
    }
}
