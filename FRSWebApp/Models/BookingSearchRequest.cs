using System;
using System.ComponentModel.DataAnnotations;

namespace Cares.WebApp.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BookingSearchRequest
    {
        /// <summary>
        /// Operation WorkPlace Code
        /// </summary>
        [Required(ErrorMessage = "Please enter Location.")]
        public string OperationWorkPlaceCode { get; set; }

        /// <summary>
        /// Operation WorkPlace ID
        /// </summary>
         [Required]
        public long OperationWorkPlaceId { get; set; }

        /// <summary>
        /// Start Date
        /// </summary>
        [Required]
        public DateTime StartDt { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        [Required]
        public DateTime EndDt { get; set; }
    }
}