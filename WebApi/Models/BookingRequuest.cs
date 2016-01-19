using System;

namespace Cares.WebApi.Models
{
    public class BookingRequuest
    {
        /// <summary>
        /// Operation WorkPlace ID
        /// </summary>
        public long OperationWorkPlaceId { get; set; }

        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime StartDt { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        public DateTime EndDt { get; set; }

        /// <summary>
        /// Tarrif Type Code
        /// </summary>
        public string TariffTypeCode { get; set; }

        /// <summary>
        /// Insurance Type Id
        /// </summary>
        public string InsuranceTypeId { get; set; }

        /// <summary>
        /// Driver Charge Rate
        /// </summary>
        public double AdditionalDriverChargeRate { get; set; }

        /// <summary>
        /// Additional Driver Name
        /// </summary>
        public string AdditionalDriverName { get; set; }

        /// <summary>
        /// Driver License Number 
        /// </summary>
        public string AdditionalDriverLicenseNumber { get; set; }

        /// <summary>
        /// Aditional Driver License Expiry date
        /// </summary>
        public DateTime LicenseExpirydate { get; set; }

        /// <summary>
        /// Designation Grade
        /// </summary>
        public string DesignationGrade { get; set; }

        /// <summary>
        /// Chauffer Charge Rate
        /// </summary>
        public string ChaufferChargeRate { get; set; }

        /// <summary>
        /// Hire Group Detail Id 
        /// </summary>
        public long HireGroupDetailId { get; set; }
    }
}