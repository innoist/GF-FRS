using System;

namespace Cares.WebApp.Models
{
    /// <summary>
    /// Booking Main info 
    /// </summary>
    public class BookingMainInfo
    {
        public long OperationWorkPlaceId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndtDateTime { get; set; }
        public string TariffTypeCode { get; set; }
        public long HireGroupDetailId { get; set; }
    }
}