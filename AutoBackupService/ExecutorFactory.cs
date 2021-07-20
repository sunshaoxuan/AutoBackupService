using AutoBackupService.Executor;
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
            }

            return executor;
        }
    }
}
