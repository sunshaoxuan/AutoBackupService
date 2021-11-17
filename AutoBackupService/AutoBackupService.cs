using AutoBackupService.Executor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace AutoBackupService
{
    public partial class AutoBackupService : ServiceBase
    {
        public bool DEBUGGING = false;
        public const string CopyTaskType = "COPY";
        public const string GitTaskType = "GIT";

        private const string IniFilename = "TaskData.ini";
        private const string TaskPrefix = "TASK";
        private string TaskFileHash = "";
        private List<TaskBaseVO> TaskList = null;

        protected List<int> RunningTasks = new List<int>();

        public AutoBackupService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //初始化
            InitService();

            Logger.WriteLog("SERVICE", "Inital finished.");
        }

        private List<TaskBaseVO>  ReadTasks()
        {
            //取当前可执行文件位置
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory;
            filePath = Path.Combine(new string[] { filePath, IniFilename });

            //第一次进入及配置文件有修改才读取任务清单
            string fileHash = PublicUtils.GetHash(filePath);
            if (fileHash.Equals(TaskFileHash))
            {
                return TaskList;
            }
            else
            {
                TaskFileHash = fileHash;
            }

            Logger.WriteLog("SERVICE", "Start reading tasks.");

            List<TaskBaseVO> tasks = new List<TaskBaseVO>();
            string[] sectionNames = INIOperationClass.INIGetAllSectionNames(filePath);
            Logger.WriteLog("SERVICE", "Task count: " + (sectionNames == null ? "0"  : sectionNames.Length.ToString()));

            foreach (string secName in sectionNames)
            {
                if (secName.ToUpper().Contains(TaskPrefix))
                {
                    string taskType = INIOperationClass.INIGetStringValue(filePath, secName, "TaskType", string.Empty);
                    //Logger.WriteLog("SERVICE", "Type of task [" + secName+"]: " + taskType);

                    TaskBaseVO task = TaskFactory.CreateTaskVO(taskType);

                    //Logger.WriteLog("SERVICE", "Base task created");

                    if (task != null)
                    {
                        string[] keys = INIOperationClass.INIGetAllItemKeys(filePath, secName);

                        //Logger.WriteLog("SERVICE", "Task key count:" + (keys == null ? "0" : keys.Length.ToString()));

                        foreach (string key in keys)
                        {
                            string value = INIOperationClass.INIGetStringValue(filePath, secName, key, string.Empty);

                            //Logger.WriteLog("SERVICE", "Value of task key [" + key+"]: " + value);

                            task.InitTaskKeyValue(key, value);
                        }

                        task.SessionRunTimes = 0;
                        tasks.Add(task);
                    }

                }
            }

            Logger.WriteLog("SERVICE", tasks.Count.ToString() + " task(s) created.");
            return tasks;
        }

        public void RunCheck(object source, System.Timers.ElapsedEventArgs e)
        {
            TaskList  = ReadTasks();

            foreach(TaskBaseVO task in TaskList)
            {
                if (DEBUGGING)
                {
                    RunTask(task);
                }
                else
                {
                    if (task.LastRunTime != null)
                    {
                        if (task.LastRunTime.AddMilliseconds(task.Interval).CompareTo(DateTime.Now) > 0)
                        {
                            //未到执行时间跳过
                            continue;
                        }
                    }

                    //没在已执行列表中的才可以执行，暂不支持多线程执行同一任务
                    if (!RunningTasks.Contains(task.GetHashCode()))
                    {
                        RunTasksWithNewThread(task);
                    }
                }
            }
        }

        private void RunTasksWithNewThread(TaskBaseVO task)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (sender, args) => {
                RunningTasks.Add(task.GetHashCode());
                RunTask(task);
            };

            worker.RunWorkerCompleted += (sender, args1) =>
            {
                RunningTasks.Remove(task.GetHashCode());
            };

            worker.RunWorkerAsync();
        }

        private void RunTask(TaskBaseVO task)
        {
            ExecutorBase executor = ExecutorFactory.CreateExecutor(task);
            executor.TaskVO = task;
            executor.Execute();
        }

        private void InitService()
        {
            EventLog.WriteEntry("AutoBackupService Started.");//在系统事件查看器里的应用程序事件里来源的描述
            Logger.WriteLog("SERVICE", "Service Started.");
            System.Timers.Timer exeTimer = new System.Timers.Timer
            {
                Interval = 600000 //执行间隔（毫秒）
            };
            Logger.WriteLog("SERVICE", "Set task scan interval as " + exeTimer.Interval.ToString()+"(ms).");
            exeTimer.Elapsed += new System.Timers.ElapsedEventHandler(RunCheck);//到达时间的时候执行事件； 
            exeTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            Logger.WriteLog("SERVICE", "Set AutoReset as " + exeTimer.AutoReset);
            exeTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 
            Logger.WriteLog("SERVICE", "Timer enabled.");
        }

        protected override void OnStop()
        {
            Logger.WriteLog("SERVICE", "Service stopped.");
            EventLog.WriteEntry("AutoBackupService Stopped.");//在系统事件查看器里的应用程序事件里来源的描述
        }
    }
}
