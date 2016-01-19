
namespace Cares.WebApp.Models
{
    /// <summary>
    /// Booking Main Request to save booking main contents
    /// </summary>
    public class WebApiBookingMainRequest
    {
        /// <summary>
        /// List of selected Insurance Types Id
        /// </summary>
        public int[] InsurancesTypeIndex { get; set; }

        /// <summary>
        /// List of selected Chauffers Ids
        /// </summary>
        public int[] ChauffersIndexInts { get; set; }

        /// <summary>
        /// Additoinal Driver info
        /// </summary>
        public AdditionalDriverInfo  AdditionalDriver { get; set; }
        /// <summary>
        /// Booking main info 
        /// </summary>
        public BookingMainInfo BookingMainInfo { get; set; }
    }
}