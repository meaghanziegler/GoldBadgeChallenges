using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsuranceBadges
{
    public class BadgeRepository
    {
        Dictionary<int, Badge> Badges = new Dictionary<int, Badge>();
        
        

        public bool AddBadgeToDirectory(Badge badge)
        {
            int count=0;
            count++;
            badge.BadgeID = count; 
            Badges.Add(count, badge);
            return true;
        }

        public Dictionary<int, Badge> GetBadge()
        {
            return Badges;
        }

        public Badge GetBadgeByID(int badgeId)
        {
            foreach (var badge in Badges)
            {
                if(badge.Key == badgeId)
                {
                    return badge.Value;
                }
            }
            return null;
        }

        public bool UpdateExistingBadgeByID(int originalBadge, Badge badge)
        {
            Badge oldBadge = GetBadgeByID(originalBadge);

            if (oldBadge != null)
            {
                oldBadge.BadgeID = badge.BadgeID;
                oldBadge.Doors = badge.Doors;

                return true;
            }
            else
                return false;
        }

        public bool DeleteExistingBadge (int existingBadge)
        {
            bool deleteResult = Badges.Remove(existingBadge);
            return deleteResult;
        }
    }
}
