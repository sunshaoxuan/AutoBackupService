using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService
{
    public class CopyTaskVO : TaskBaseVO
    {
        /// <summary>
        /// 任务执行方式
        /// </summary>
        public TaskMethodEnum Method { get; set; } = TaskMethodEnum.Current; //默认复制当前文件夹（不递归子文件夹）

        /// <summary>
        /// 来源路径
        /// </summary>
        public string SourcePath { get; set; }

        /// <summary>
        /// 文件过滤
        /// </summary>
        public string[] FilePattern { get; set; } = new string[] { "*.*" }; //默认所有文件

        /// <summary>
        /// 文件夹过滤
        /// </summary>
        public string[] DirPattern { get; set; } = new string[] { "*" }; //默认所有文件夹

        /// <summary>
        /// 目标路径
        /// </summary>
        public string TargetPath { get; set; }

        /// <summary>
        /// 排除文件（不支持通配符）
        /// </summary>
        public string[] FileExclude { get; set; } = new string[] { "" }; //默认不排除

        /// <summary>
        /// 排除文件夹（不支持通配符）
        /// </summary>
        public string[] DirExclude { get; set; } = new string[] { "" }; //默认不排除


        public new void InitTaskKeyValue(string key, string value)
        {
            base.InitTaskKeyValue(key, value);

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
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.ToUpper().Equals("RECURSION"))
                    {
                        Method = TaskMethodEnum.Recursion;
                    }
                    else if (value.ToUpper().Equals("CURRENT"))
                    {
                        Method = TaskMethodEnum.Current;
                    }
                }
            }

            if (key.ToUpper().Equals("FILEPATTERN"))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    FilePattern = value.Split(new char[] { ';', ',', ' ', '\t' });
                }
            }

            if (key.ToUpper().Equals("DIRPATTERN"))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DirPattern = value.Split(new char[] { ';', ',', ' ', '\t' });
                }
            }

            if (key.ToUpper().Equals("DIREXCLUDE"))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DirExclude = value.Split(new char[] { ';', ',', ' ', '\t' });
                }
            }

            if (key.ToUpper().Equals("FILEEXCLUDE"))
            {
                if (!string.IsNullOrEmpty(value))
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