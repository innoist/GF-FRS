using FRS.Models.DomainModels;

namespace FRS.Interfaces.Repository
{
    public interface ILoadRepository : IBaseRepository<Load, long>
    {
        Load GetLoad(long mt940LoadId);
    }
}
