using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoClaimsDepartment
{
    public class ClaimRepository
    {
        protected readonly List<Claim> _claimDirectory = new List<Claim>();

        public Claim NextClaim()
        {
            return _claimDirectory[0];
        }
        public bool AddClaimToDirectory(Claim claim)
        {
            int startingCount = _claimDirectory.Count;
            _claimDirectory.Add(claim);

            bool wasAdded = (_claimDirectory.Count > startingCount) ? true : false;
            return wasAdded;
        }

        public List<Claim> GetClaims()
        {
            return _claimDirectory;
        }

        public Claim GetClaimById(int claimId)
        {
            foreach (Claim claim in _claimDirectory)
            {
                if(claim.ClaimID == claimId)
                {
                    return claim;
                }
            }
            return null;
        }

        public bool UpdateExistingClaimById(int originalID, Claim claim)
        {
            Claim oldClaim = GetClaimById(originalID);

            if (oldClaim != null)
            {
                oldClaim.ClaimID = claim.ClaimID;
                oldClaim.TypeOfClaim = claim.TypeOfClaim;
                oldClaim.Description = claim.Description;
                oldClaim.ClaimAmount = claim.ClaimAmount;
                oldClaim.DateOfIncident = claim.DateOfIncident;
                oldClaim.DateOfClaim = claim.DateOfClaim;

                return true;
            }
            else
                return false;
        }

        public bool DeleteExistingClaim(Claim existingClaim)
        {
            bool deleteResult = _claimDirectory.Remove(existingClaim);
            return deleteResult;
        }
    }
}
