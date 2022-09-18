using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QEModList.Core
{
    public static class QEModListPaths
    {
        public static string GetProgramAppDataFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JPiolho", "QEModList");
        }
    }
}
