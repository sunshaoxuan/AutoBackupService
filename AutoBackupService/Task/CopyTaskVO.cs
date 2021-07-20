using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService
{
    public class CopyTaskVO : TaskBaseVO
    {
        public TaskMethodEnum Method { get; set; }
        public string SourcePath { get; set; }
        public string[] SourceFileFilters { get; set; }
        public string TargetPath { get; set; }


        public override void InitTaskKeyValue(string key, string value)
        {
            if (key.ToUpper().Equals("TASKNAME"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("任务名称 [TaskName] 不能为空");
                }
                TaskName = value;
            }

            if (key.ToUpper().Equals("CREATEDTIME"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("任务创建时间 [CreatedTime] 不能为空");
                }
                CreatedTime = DateTime.Parse(value);
            }

            if (key.ToUpper().Equals("MODIFIEDTIME"))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ModifiedTime = DateTime.Parse(value);
                }
            }

            if (key.ToUpper().Equals("SOURCEPATH"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("任务来源文件夹 [SourcePath] 不为能空");
                }
                else
                {
                    SourcePath = value;
                }
            }

            if (key.ToUpper().Equals("TARGETPATH"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("任务目标文件夹 [TargetPath] 不为能空");
                }
                else
                {
                    TargetPath = value;
                }
            }

            if (key.ToUpper().Equals("METHOD"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    Method = TaskMethodEnum.Current;
                }
                else
                {
                    if (value.ToUpper().Equals("RECU"))
                    {
                        Method = TaskMethodEnum.Recursion;
                    }
                    else if (value.ToUpper().Equals("SINGLE"))
                    {
                        Method = TaskMethodEnum.Current;
                    }
                    else
                    {
                        Method = TaskMethodEnum.Current;
                    }
                }
            }

            if (key.ToUpper().Equals("SOURCEFILEFILTERS"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    SourceFileFilters = new string[] { "*.*" };
                }
                else
                {
                    SourceFileFilters = value.Split(new char[] { ';', ',', ' ', '\t' });
                }
            }
        }


        public override int GetHashCode()
        {
            string hashSeed = TaskName + SourcePath + string.Concat(SourceFileFilters.ToArray()) + TargetPath;
            return hashSeed.GetHashCode();
        }

        //<summary>
        //任务执行方法
        //</summary>
        public enum TaskMethodEnum
        {
            //当前文件夹
            Current,
            //递归所有文件夹
            Recursion
        }
    }
}