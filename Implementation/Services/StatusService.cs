using System.Collections.Generic;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;

namespace FRS.Implementation.Services
{
    public class StatusService:IStatusService
    {
        private readonly IStatusRepository statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            this.statusRepository = statusRepository;
        }

        public IEnumerable<Status> GetStatuses()
        {
            return statusRepository.GetAll();
        }

        public bool SaveStatus(Status status)
        {
            if (status.Value == 0)
            {
                statusRepository.Add(status);
            }
            else
            {
                statusRepository.Update(status);
            }
            statusRepository.SaveChanges();
            return true;
        }

        public Status GetStatus(int Id)
        {
            return statusRepository.Find(Id);
        }
    }
}
