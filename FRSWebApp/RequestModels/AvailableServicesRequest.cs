using System;

namespace Cares.WebApp.RequestModels
{
    /// <summary>
    /// Avaliable Services Request Model
    /// </summary>
    public class AvailableServicesRequest
    {
        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Operation Workplace Id
        /// </summary>
        public long OperationWorkplaceId { get; set; }

        /// <summary>
        /// Hire Group Detail Id
        /// </summary>
        public long HireGroupDetailId { get; set; }
    }
}