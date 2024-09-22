using ReadLog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.MVVM.Models
{
    public class Manga
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
        public string CoverART_ID { get; set; }
        public string CoverArt_Path { get; set; }

    }
}
