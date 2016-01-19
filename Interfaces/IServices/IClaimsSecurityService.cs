using System;
using System.Security.Claims;

namespace FRS.Interfaces.IServices
{
    /// <summary>
    /// Service that adds security claims to the identity
    /// </summary>
    public interface IClaimsSecurityService
    {
        /// <summary>
        /// Adds user claims to Identity
        /// </summary>
        void AddClaimsToIdentity(long domainKey, string defaultRole, string userName, TimeSpan userTimeZoneOffset, ClaimsIdentity identity);

    }
}
