using System.Collections.Generic;

namespace Cares.WebApp.Models
{
    /// <summary>
    /// Get Operation Workplace Result
    /// </summary>
    public class GetOperationWorkplaceResult
    {
        /// <summary>
        /// Web Api Operation Workplace
        /// </summary>
        public IList<WebApiOperationWorkplace> OperationWorkplaces { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        public string Error { get; set; }
    }
}