using System.Collections.Generic;
using System.Linq;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;

namespace FRS.Implementation.Services
{
    public class LoadTypeService : ILoadTypeService
    {
        private readonly ILoadTypeRepository loadTypeRepository;
        public LoadTypeService(ILoadTypeRepository loadTypeRepository)
        {
            this.loadTypeRepository = loadTypeRepository;
        }


        public IEnumerable<LoadType> GetLoadTypes()
        {
            return loadTypeRepository.GetAll().ToList();
        }
    }
}
