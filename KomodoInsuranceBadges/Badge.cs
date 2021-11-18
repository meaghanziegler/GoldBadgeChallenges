using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsuranceBadges
{
    public class Badge
    {
        public Badge() { }
        public Badge(List<int> badgeId, List<string> doors)
        {
            BadgeID = badgeId;
            Doors = doors;
        }

        public List<int> BadgeID { get; }
        public List<string> Doors { get; }
    }
}
