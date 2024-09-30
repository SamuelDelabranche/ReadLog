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
        private string _id = "Not Found";
        private string _name = "None";
        private string _description = "Not Found";
        private string _coverART_ID = "Not Found";
        private string _coverART_PATH = "Not Found";
        private bool _isFavorite = false;
        private int _nombreChapitreLus = 0;

        public string ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public string CoverArt_ID { get => _coverART_ID; set => _coverART_ID = value; }
        public string CoverArt_Path { get => _coverART_PATH; set => _coverART_PATH = value; }
        public bool IsFavorite { get => _isFavorite; set => _isFavorite = value; }
        public int NombreChapitreLus { get => _nombreChapitreLus; set => _nombreChapitreLus = value; }
    }
}
