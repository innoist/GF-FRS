using System.Collections.Generic;
using Cares.Interfaces.IServices;
using Cares.Interfaces.Repository;
using Cares.Models.CommonTypes;
using Cares.Models.DomainModels;
using System.Security.Claims;
using Cares.Models.IdentityModels;

namespace Cares.Common
{
    /// <summary>
    /// Service that manages security claims
    /// </summary>
    public class ClaimsSecurityService :  IClaimsSecurityService
    {
        #region Private

        private IDomainLicenseDetailsRepository domainLicenseDetailsRepository;
       
        #endregion
        #region Constructor

        public ClaimsSecurityService(IDomainLicenseDetailsRepository domainLicenseDetailsRepository)
        {
            this.domainLicenseDetailsRepository = domainLicenseDetailsRepository;
        }

        #endregion
        #region Public


        /// <summary>
        /// Add Organisation Claims
        /// </summary>
        private void AddDomainLicenseDetailClaims(double domainKey)
        {
               DomainLicenseDetail domainLicenseDetail =
                   domainLicenseDetailsRepository.GetDomainLicenseDetailByDomainKey(domainKey);
            if (domainLicenseDetail != null)
            {
                ClaimHelper.AddClaim(new Claim(CaresUserClaims.DomainLicenseDetail,
                    ClaimHelper.Serialize(
                        new DomainLicenseDetailClaim
                        {
                            UserDomainKey = domainLicenseDetail.UserDomainKey,
                            Branches = domainLicenseDetail.Branches,
                            FleetPools = domainLicenseDetail.FleetPools,
                            Employee = domainLicenseDetail.Employee,
                            RaPerMonth = domainLicenseDetail.RaPerMonth,
                            Vehicles = domainLicenseDetail.Vehicles
                        }),
                    typeof(DomainLicenseDetailClaim).AssemblyQualifiedName));
            }
        }
        
        /// <summary>
        /// Add claims to the identity
        /// </summary>
        public void AddClaimsToIdentity(User user, ClaimsIdentity identity)
        {
            ClaimHelper.SetIdentity(identity);
            ClaimHelper.AddClaim(new Claim(CaresUserClaims.UserDomainKey, user.UserDomainKey.ToString()));
            AddDomainLicenseDetailClaims(user.UserDomainKey);
            ClaimHelper.SetCurrentPrincipal();
            IList<DomainLicenseDetailClaim> claim =
               ClaimHelper.GetClaimsByType<DomainLicenseDetailClaim>(CaresUserClaims.DomainLicenseDetail);
        
        }
        #endregion
    }
}
