using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService
{
    public class TaskFactory
    {
        public static TaskBaseVO CreateTaskVO(string taskType)
        {
            TaskBaseVO taskVO = null;

            if (taskType.Equals(AutoBackupService.CopyTaskType))
            {
                taskVO= new CopyTaskVO();
            }

            return taskVO;
        }
    }
}
