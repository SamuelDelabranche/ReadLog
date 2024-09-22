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
        public Manga(string name, string iD, bool favorite = false, string description = "None", string coverART_ID = "None", string coverArt_Path = "None")
        {
            Name = name;
            ID = iD;
            Description = description;
            CoverART_ID = coverART_ID;
            CoverArt_Path = coverArt_Path;
            isFavorite = favorite;
        }

        public string Name { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
        public string CoverART_ID { get; set; }
        public string CoverArt_Path { get; set; }
        public bool isFavorite { get; set; }

    }
}
