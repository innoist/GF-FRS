using System.Collections.Generic;

namespace Cares.WebApp.Models
{

    /// <summary>
    /// Get Available Hire Group Result
    /// </summary>
    public class GetHireGroupResult
    {
        /// <summary>
        /// Web Api Hire Group
        /// </summary>
        public IList<WebApiHireGroup> AvailableHireGroups { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        public string Error { get; set; }
    }
}