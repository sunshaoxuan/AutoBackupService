using AutoBackupService.Executor;
using AutoBackupService.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService
{
    public class ExecutorFactory
    {
        public static ExecutorBase CreateExecutor(TaskBaseVO task)
        {
            ExecutorBase executor = null;

            if (task is CopyTaskVO)
            {
                executor = new CopyTaskExecutor();
            }else if (task is GitTaskVO)
            {
                executor = new GitTaskExecutor();
            }

            return executor;
        }
    }
}
