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

        public abstract void BeforeExecute();

        public void Execute()
        {
            BeforeExecute();

            try { 
            DoExecute();
            }catch(Exception ex)
            {
                Logger.WriteLog("TASK", "ERROR occored on execute the task:" + ex.Message);
            }

            AfterExecute();
        }

        public abstract void DoExecute();

        public abstract void AfterExecute();

        protected static string GetHash(string path)
        {
            return PublicUtils.GetHash(path);
        }
    }
}
