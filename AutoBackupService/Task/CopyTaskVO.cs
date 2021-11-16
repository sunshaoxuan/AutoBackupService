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
        public string[] FilePattern { get; set; }
        public string[] DirPattern { get; set; }
        public string TargetPath { get; set; }
        public string[] FileExclude { get; set; }
        public string[] DirExclude { get; set; } 


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

            if (key.ToUpper().Equals("INTERVAL"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    Interval = 60000;
                }
                else
                {
                    bool gotnum = Int64.TryParse(value, out long longNum);
                    if (gotnum)
                    {
                        Interval = longNum;
                    }
                    else
                    {
                        Interval = 60000;
                    }
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
                    if (value.ToUpper().Equals("RECURSION"))
                    {
                        Method = TaskMethodEnum.Recursion;
                    }
                    else if (value.ToUpper().Equals("CURRENT"))
                    {
                        Method = TaskMethodEnum.Current;
                    }
                    else
                    {
                        Method = TaskMethodEnum.Current;
                    }
                }
            }

            if (key.ToUpper().Equals("FILEPATTERN"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    FilePattern = new string[] { "*.*" };
                }
                else
                {
                    FilePattern = value.Split(new char[] { ';', ',', ' ', '\t' });
                }
            }

            if (key.ToUpper().Equals("DIRPATTERN"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    DirPattern = new string[] { "*" };
                }
                else
                {
                    DirPattern = value.Split(new char[] { ';', ',', ' ', '\t' });
                }
            }

            if (key.ToUpper().Equals("DIREXCLUDE"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    DirExclude = new string[] { "*" };
                }
                else
                {
                    DirExclude = value.Split(new char[] { ';', ',', ' ', '\t' });
                }
            }

            if (key.ToUpper().Equals("FILEEXCLUDE"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    FileExclude = new string[] { "*" };
                }
                else
                {
                    FileExclude = value.Split(new char[] { ';', ',', ' ', '\t' });
                }
            }
        }


        public override int GetHashCode()
        {
            string hashSeed = TaskName + SourcePath + string.Concat(FilePattern.ToArray()) + TargetPath;
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