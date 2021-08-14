using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class CommunityAdvertisement
    {
        public string CommunityID { get; set; }
        public int AdvertisementID { get; set; }

        public Community Community { get; set; }

        public Advertisements Advertisements { get; set; }
    }
}
