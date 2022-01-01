using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService.Task
{
    internal class ZipTaskVO : TaskBaseVO
    {
        /// <summary>
        /// 源文件清单
        /// </summary>
        public string[] SourceFileFullPaths { get; set; } = new string[] { "" };

        /// <summary>
        /// 压缩文件完成路径
        /// </summary>
        public string TargetZipFileFullPath { get; set; } = "";

        /// <summary>
        /// 是否需要加密
        /// </summary>
        public bool NeedEncrypt { get; set; } = false;

        /// <summary>
        /// 当NeedEncrypt为true时使用的密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        public new void InitTaskKeyValue(string key, string value)
        {
            base.InitTaskKeyValue(key, value);

            if (key.ToUpper().Equals("SOURCEFILEFULLPATHLIST"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("源文件清单 [SourceFileFullPathList] 不能为空。");
                }
                else
                {
                    SourceFileFullPaths = value.Split(new char[] { ';', ',', ' ', '\t' });
                }
            }

            if (key.ToUpper().Equals("TARGETZIPFILEFULLPATH"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("目标Zip文件全路径 [TargetZipFileFullPath] 不能为空。");
                }
                else
                {
                    TargetZipFileFullPath = value;
                }
            }

            if (key.ToUpper().Equals("NEEDENCRYPT"))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    NeedEncrypt = Boolean.Parse(value);
                }
            }

            if (key.ToUpper().Equals("PASSWORD"))
            {
                if (string.IsNullOrEmpty(value))
                {
                    Password = "";
                }
                else
                {
                    Password = value;
                }
            }
        }
    }
}
