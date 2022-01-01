using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService
{
    public class PublicUtils
    {
        public  static string GetFileHash(string path)
        {
            var hash = SHA1.Create();
            var stream = new FileStream(path, FileMode.Open);
            byte[] hashByte = hash.ComputeHash(stream);
            stream.Close();
            return BitConverter.ToString(hashByte).Replace("-", "");
        }

        public static string GetDirectoryHash(string path)
        {
            if (Directory.Exists(path))
            {
                string hashCodeDir = "";
                foreach (string dirPath in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
                {
                    if (Directory.Exists(dirPath))
                    {
                        hashCodeDir = BitConverter.ToString(Encoding.ASCII.GetBytes((new DirectoryInfo(dirPath)).Name.GetHashCode().ToString()));
                        foreach (string filePath in Directory.GetFiles(dirPath, "*", SearchOption.TopDirectoryOnly))
                        {
                            if (File.Exists(filePath))
                            {
                                string hashCodeFile = GetFileHash(filePath);
                                hashCodeDir = BitConverter.ToString(Encoding.ASCII.GetBytes((hashCodeDir+hashCodeFile).GetHashCode().ToString()));
                            }
                        }
                    }
                }
                return hashCodeDir;
            }
            else
            {
                return "";
            }
        }
    }
}
