using System.Collections.Generic;
using System.Linq;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class LoadTypeService : ILoadTypeService
    {
        private readonly ILoadTypeRepository loadTypeRepository;
        private readonly IStatusRepository statusRepository;
        public LoadTypeService(ILoadTypeRepository loadTypeRepository, IStatusRepository statusRepository)
        {
            this.loadTypeRepository = loadTypeRepository;
            this.statusRepository = statusRepository;
        }


        public IEnumerable<LoadType> GetLoadTypes()
        {
            return loadTypeRepository.GetAll().ToList();
        }

        public LoadType GetLoadType(int Id)
        {
            return loadTypeRepository.Find(Id);
        }

        public LoadTypeBaseData GetLoadTypeBaseData(int ? Id)
        {
            var baseData = new LoadTypeBaseData();
            if (Id != null)
            {
                baseData.LoadType = loadTypeRepository.Find((int) Id);
            }
            baseData.Statuses = statusRepository.GetAll().ToList();

            return baseData;
        }

        public bool SaveLoadType(LoadType loadType)
        {
            if (loadType.Value == 0)
            {
                loadTypeRepository.Add(loadType);
            }
            else
            {
                loadTypeRepository.Update(loadType);
            }
            loadTypeRepository.SaveChanges();
            return true;
        }
    }
}
