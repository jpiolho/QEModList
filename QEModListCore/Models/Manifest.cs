using System.Collections.Generic;

namespace QEModList.Core.Models
{
    public class Manifest
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Gamedir { get; set; }
        public string Download { get; set; }
        public List<string> Screenshots { get; set; }

        public string Pak { get; set; }
        public int Size { get; set; }
    }

}
