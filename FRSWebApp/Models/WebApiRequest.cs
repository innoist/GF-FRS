
using System;

namespace Cares.WebApp.Models
{
    public class WebApiRequest
    {
        /// <summary>
        /// Start Date Time
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// End Date Time
        /// </summary>
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// Out Location OpeartionWorkplaceId
        /// </summary>
        public long OutLocationId { get; set; }

        /// <summary>
        /// Tarrif Type Id 
        /// </summary>
        public string TarrifTypeCode { get; set; }

        /// <summary>
        /// Domain Key
        /// </summary>
        public long DomainKey { get; set; }
    }
}