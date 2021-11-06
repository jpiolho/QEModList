using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QEModList.Core.Models
{
    public class Addon
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public int Size { get; set; }
        public Dictionary<string, string> Description { get; set; } = new Dictionary<string, string>();
        public string Gamedir { get; set; }
        public string Download { get; set; }
        public List<string> Screenshots { get; set; } = new List<string>();
        public string Id { get; set; }
    }
}
