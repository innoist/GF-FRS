using Cares.WebApp.Models;

namespace Cares.WebApp.ModelsMapper
{
    /// <summary>
    /// 
    /// </summary>
    public static class AvailableHireGroupMapper
    {
        public static HireGroupDetail CreateFrom(this WebApiHireGroup source)
        {
            return new HireGroupDetail
            {
                VehilceId = source.VehilceId,
                NumberPlate = source.NumberPlate,
                HireGroupId= source.HireGroupId,
                HireGroupName = source.HireGroupName,
                AllowedMileage = source.AllowedMileage,
                HireGroupDetailId = source.HireGroupDetailId,
                VehicleMakeName = source.VehicleMakeName,
                VehilceModelName = source.VehilceModelName,
                VehicleCategoryName = source.VehicleCategoryName,
                ModelYear = source.ModelYear,
                RentalCharge = source.RentalCharge,
                ImageSource = source.ImageSource,
                TariffTypeCode = source.TariffTypeCode
            };
        }
    }
}