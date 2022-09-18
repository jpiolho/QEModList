using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace QEModList
{
    public class Options
    {
        public int Version { get; set; }
        public string QuakeLaunchPath { get; set; } = "STEAM";
        public string QuakeLaunchArguments { get; set; } = "";
        public bool RefreshLocalAddons { get; set; } = false;
    }
}
