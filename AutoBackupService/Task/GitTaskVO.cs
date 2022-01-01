using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService.Task
{
    internal class GitTaskVO : TaskBaseVO
    {
        /// <summary>
        /// 任务执行方法
        /// </summary>
        public TaskMethodEnum Method { get; set; }

        /// <summary>
        /// Git根存储绝对路径
        /// </summary>
        public string RepositoryAbsolutePath { get; set; }

        /// <summary>
        /// 远程引用根路径
        /// </summary>
        public string RemoteRefRoot { get; set; }

        /// <summary>
        /// 分支
        /// </summary>
        public string Branch { get; set; }

        public new void InitTaskKeyValue(string key, string value)
        {
            base.InitTaskKeyValue(key, value);

            if (key.ToUpper().Equals("METHOD"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    Method = TaskMethodEnum.PULL;
                }
                else
                {
                    if (value.ToUpper().Equals("PULL"))
                    {
                        Method = TaskMethodEnum.PULL;
                    }
                    else if (value.ToUpper().Equals("COMMIT"))
                    {
                        Method = TaskMethodEnum.COMMIT;
                    }
                    else if (value.ToUpper().Equals("PUSH"))
                    {
                        Method = TaskMethodEnum.PUSH;
                    }
                    else
                    {
                        Method = TaskMethodEnum.PULL;
                    }
                }
            }

            if (key.ToUpper().Equals("REPOSITORYABSOLUTEPATH"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Git根存储路径 [RepositoryAbsolutePath] 不能为空");
                }
                RepositoryAbsolutePath = value;
            }

            if (key.ToUpper().Equals("REMOTEREFROOT"))
            {
                RemoteRefRoot = value;
            }

            if (key.ToUpper().Equals("BRANCH"))
            {
                Branch = value;
            }
        }

        //<summary>
        //任务执行方法
        //</summary>
        public enum TaskMethodEnum
        {
            //拉取
            PULL,
            //提交
            COMMIT,
            //推送
            PUSH
        }
    }
}
