namespace Cares.WebApp.Models
{
    /// <summary>
    /// Hire Group Detail
    /// </summary>
    public class HireGroupDetail
    {
        /// <summary>
        /// Hiregroup detail Id
        /// </summary>
        public long HireGroupDetailId { get; set; }

        /// <summary>
        /// Vehicle Make Cod
        /// </summary>
        public string VehicleMakeName { get; set; }

        /// <summary>
        /// Vehicle Model name
        /// </summary>
        public string VehilceModelName { get; set; }

        /// <summary>
        /// Vehicle Category Name
        /// </summary>
        public string VehicleCategoryName { get; set; }

        /// <summary>
        /// Vehicle Model year
        /// </summary>
        public short ModelYear { get; set; }


        /// <summary>
        /// Vehicle Rental Charge
        /// </summary>

        public float RentalCharge { get; set; }
        
        /// <summary>
        /// Vehicle Image source
        /// </summary>
        public string ImageSource { get; set; }
        public double HireGroupId { get; set; }
        public double AllowedMileage { get; set; }
        public string HireGroupName { get; set; }
        public long VehilceId { get; set; }
        public string NumberPlate { get; set; }
        public string TariffTypeCode { get; set; }


    }
}