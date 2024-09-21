using ReadLog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.MVVM.Models
{
    public class Manga : ItemBase
    {
        public string CoverART_ID { get; set; }
        public string CoverArt_Path { get; set; }
    }
}
