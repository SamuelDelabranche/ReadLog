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
        private string _nombreChapitreTotaux = "0";


        public string ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public string CoverArt_ID { get => _coverART_ID; set => _coverART_ID = value; }
        public string CoverArt_Path { get => _coverART_PATH; set => _coverART_PATH = value; }
        public bool IsFavorite { get => _isFavorite; set => _isFavorite = value; }
        public int NombreChapitreLus { get => _nombreChapitreLus; set => _nombreChapitreLus = value; }
        public string NombreChapitreTotaux { get => _nombreChapitreTotaux; set => _nombreChapitreTotaux = value; }

        public static bool operator ==(Manga m1, Manga m2)
        {
            if (ReferenceEquals(m1, m2)) return true;
            if (m1 is null || m2 is null) return false;
            return m1.Name.ToLowerInvariant() == m2.Name.ToLowerInvariant() && m1.ID == m2.ID;
        }

        public static bool operator !=(Manga m1, Manga m2) => !(m1 == m2);

        public override bool Equals(object obj)
        {
            return obj is Manga manga && this == manga;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, ID);
        }

        public override string ToString()
        {
            return $"Manga [{Name}] : " +
                $"\n- ID : {ID}" +
                $"\n- Description : {Description}" +
                $"\n- CoverArt_ID : {CoverArt_ID}" +
                $"\n- CoverArt_PATH : {CoverArt_Path}" +
                $"\n- NombreChapitreTotaux: {_nombreChapitreTotaux}";
        }
    }
}
