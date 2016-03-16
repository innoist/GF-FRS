using System.Collections.Generic;
using System.Linq;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class LoadStatusService : ILoadStatusService
    {
        private readonly ILoadStatusRepository loadStatusRepository;
        private readonly IStatusRepository statusRepository;

        public LoadStatusService(ILoadStatusRepository loadStatusRepository, IStatusRepository statusRepository)
        {
            this.loadStatusRepository = loadStatusRepository;
            this.statusRepository = statusRepository;
        }


        public IEnumerable<LoadStatu> GeLoadStatuses()
        {
            return loadStatusRepository.GetAll().ToList();
        }

        public LoadStatusResponse GetLoadStatus(int? Id)
        {
            LoadStatusResponse response = new LoadStatusResponse
            {
                Statuses = statusRepository.GetAll().ToList()
            };
            if (Id != null)
            {
                response.LoadStatus = loadStatusRepository.Find((int) Id);
            }
            return response;
        }

        public bool SaveLoadStatus(LoadStatu currency)
        {
            if (currency.Value == 0)
            {
                loadStatusRepository.Add(currency);
            }
            else
            {
                loadStatusRepository.Update(currency);
            }
            loadStatusRepository.SaveChanges();
            return true;
        }
    }
}
