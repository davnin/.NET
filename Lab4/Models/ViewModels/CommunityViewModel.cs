using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models.ViewModels
{
    public class CommunityViewModel
    {
        public IEnumerable <Student> Student { get; set; }
        public IEnumerable <Community> Communities { get; set; }
        public IEnumerable <CommunityMembership> Memberships { get; set; }
        public IEnumerable<Advertisements> Advertisements { get; set; }
        public IEnumerable<CommunityAdvertisement> CommunityAdvertisements { get; set; }
    }
}
