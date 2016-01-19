using System;

namespace Cares.WebApp.RequestModels
{
    /// <summary>
    /// Available Hire Group Request
    /// </summary>
    public class AvailableHireGroupRequest
    {
        /// <summary>
        /// Operation Workplace Id
        /// </summary>
        public long OperationsWorkplaceId { get; set; }

        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime StartDt { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}