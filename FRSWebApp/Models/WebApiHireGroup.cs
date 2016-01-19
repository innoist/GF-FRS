using System;

namespace Cares.WebApp.Models
{
    /// <summary>
    /// Web Api Available HireGroup Result
    /// </summary>
    public class WebApiHireGroup
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
        /// Vehicle Image byte array
        /// </summary>
        public byte[] Image { get; set; }
        /// <summary>
        /// Vehicle Rental Charge
        /// </summary>

        public float RentalCharge { get; set; }
        /// <summary>
        /// Vehicle Image source
        /// </summary>
        public string ImageSource
        {
            get
            {
                if (Image == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(Image);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }
        /// <summary>
        /// User domain key
        /// </summary>
        public long DomainKey { get; set; }
        public double HireGroupId { get; set; }
        public double AllowedMileage { get; set; }
        public string HireGroupName { get; set; }
        public long VehilceId { get; set; }
        public string NumberPlate { get; set; }
        public string TariffTypeCode { get; set; }


    }
}