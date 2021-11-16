using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackupService
{
    public class UnitTest
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        static void Main()
        {
            AutoBackupService testService = new AutoBackupService();
            testService.DEBUGGING = true;
            testService.RunCheck(null, null);
        }
    }
}
