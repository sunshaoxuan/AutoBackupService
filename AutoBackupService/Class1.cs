using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService
{
    class Class1
    {
        [STAThread]
        static void Main(string[] args)
        {
            AutoBackupService srv = new AutoBackupService();
            srv.RunCheck(null, null);
        }
    }
}
