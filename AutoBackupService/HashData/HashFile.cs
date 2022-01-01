using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService.HashData
{
    public class HashFile
    {
        /// <summary>
        /// 文件Hash记录集
        /// </summary>
        public const Int16 FILE_HASH = 1;

        /// <summary>
        /// 文件夹Hash记录集
        /// </summary>
        public const Int16 DIR_HASH = 2;

        /// <summary>
        /// 复合Hash记录集
        /// </summary>
        public const Int16 MIX_HASH = 4;

        /// <summary>
        /// 文件绝对路径（不包含文件名）
        /// </summary>
        public string FileAbsolutePath;

        /// <summary>
        /// 文件完整名称（包含全路径及文件名）
        /// </summary>
        public string FileFullName;

        /// <summary>
        /// 文件名（不包含路径）
        /// </summary>
        public string FileName;

        /// <summary>
        /// 文件大小
        /// </summary>
        public Int64 FileSize;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime;

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedTime;

        /// <summary>
        /// 是否自动存储
        /// </summary>
        public bool AutoSave { get; set; }

        private bool Loaded = false;
        private bool Saved = true;

        /// <summary>
        /// 文件头结构
        /// </summary>
        public struct FileHeadStruct
        {
            /// <summary>
            /// Hash文件类型
            /// </summary>
            public Int16 HashType;

            /// <summary>
            /// 记录集数量
            /// </summary>
            public Int16 RecordCount;
        }

        /// <summary>
        /// 文件数据区结构
        /// </summary>
        public struct FileDataStruct
        {
            /// <summary>
            /// 完全路径名Hash值（包含Hash本体名称）
            /// </summary>
            public string FullPathHashCode;

           /// <summary>
           /// 内容Hash值
           /// </summary>
            public string ContentHashCode;
        }

        /// <summary>
        /// 加载文件
        /// </summary>
        public void Load()
        {
            Loaded = true;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        public void Save()
        {
            Saved = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void AppendData(FileDataStruct data)
        {
            Saved = false;
        }
    }
}
