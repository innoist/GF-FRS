using System;

namespace FRS.Models.DomainModels
{
    /// <summary>
    /// domain model
    /// </summary>
    public class DomainLicenseDetail
    {
        #region Public
        /// <summary>
        /// Domain License Details Id
        /// </summary>
        public int DomainLicenseDetailsId { get; set; }
        /// <summary>
        /// Rental Agreements per month
        /// </summary>
        public int RaPerMonth { get; set; }
        /// <summary>
        /// Employee per month
        /// </summary>
        public int Employee { get; set; }
        /// <summary>
        /// Branches per month
        /// </summary>
        public int Branches { get; set; }
        /// <summary>
        /// Number of fleet Pools
        /// </summary>
        public int FleetPools { get; set; }
        /// <summary>
        /// Number of Vehicles
        /// </summary>
        public int Vehicles { get; set; }
       
        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Is Deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Is Private
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Is Readonly
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Record Created Date
        /// </summary>
        public DateTime RecCreatedDt { get; set; }

        /// <summary>
        /// Record Created By
        /// </summary>
        public string RecCreatedBy { get; set; }

        /// <summary>
        /// Record Last Updated Date
        /// </summary>
        public DateTime RecLastUpdatedDt { get; set; }

        /// <summary>
        /// Record Last Updated By
        /// </summary>
        public string RecLastUpdatedBy { get; set; }

        /// <summary>
        /// User Domain Key
        /// </summary>
        public long UserDomainKey { get; set; }
#endregion
    }
}
