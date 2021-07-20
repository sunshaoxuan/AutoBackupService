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
        public const string CopyTaskType = "COPY";

        private const string IniFilename = "TaskData.ini";
        private const string TaskPrefix = "TASK";

        protected List<int> RunningTasks = new List<int>();

        public AutoBackupService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //初始化
            InitService();
        }

        private List<TaskBaseVO>  ReadTasks()
        {
            //取当前可执行文件位置
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory;
            filePath = Path.Combine(new string[] { filePath, IniFilename });
            List<TaskBaseVO> tasks = new List<TaskBaseVO>();
            string[] sectionNames = INIOperationClass.INIGetAllSectionNames(filePath);
            foreach (string secName in sectionNames)
            {
                if (secName.ToUpper().Contains(TaskPrefix))
                {
                    string taskType = INIOperationClass.INIGetStringValue(filePath, secName, "TaskType", string.Empty);

                    if (!string.IsNullOrEmpty(taskType))
                    {
                        if (taskType.ToUpper().Equals(CopyTaskType))
                        {
                            TaskBaseVO task = TaskFactory.CreateTaskVO(CopyTaskType);

                            if (task != null)
                            {
                                string[] keys = INIOperationClass.INIGetAllItemKeys(filePath, secName);
                                foreach (string key in keys)
                                {
                                    task.InitTaskKeyValue(key, INIOperationClass.INIGetStringValue(filePath, secName, key, string.Empty));
                                }

                                tasks.Add(task);
                            }
                        }
                    }
                }
            }

            return tasks;
        }

        public void RunCheck(object source, System.Timers.ElapsedEventArgs e)
        {
            List<TaskBaseVO> taskList = ReadTasks();

            foreach(TaskBaseVO task in taskList)
            {
                if (!RunningTasks.Contains(task.GetHashCode()))
                {
                    RunTasksWithNewThread(task);
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
            System.Timers.Timer exeTimer = new System.Timers.Timer
            {
                Interval = 5000 //执行间隔（毫秒）
            };
            exeTimer.Elapsed += new System.Timers.ElapsedEventHandler(RunCheck);//到达时间的时候执行事件； 
            exeTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            exeTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("AutoBackupService Stopped.");//在系统事件查看器里的应用程序事件里来源的描述
        }
    }
}
