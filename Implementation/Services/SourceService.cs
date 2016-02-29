using System.Collections.Generic;
using System.Linq;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.ResponseModels;

namespace FRS.Implementation.Services
{
    public class SourceService : ISourceService
    {
        private readonly ISourceRepository sourceRepository;
        private readonly IStatusRepository statusRepository;

        public SourceService(ISourceRepository sourceRepository, IStatusRepository statusRepository)
        {
            this.sourceRepository = sourceRepository;
            this.statusRepository = statusRepository;
        }

        public IEnumerable<Source> GetSources()
        {
            return sourceRepository.GetSources().ToList();
        }

        public SourceBaseData GetSourceById(int? Id)
        {
            var baseData = new SourceBaseData();
            if (Id != null)
            {
                baseData.Source = sourceRepository.Find((int) Id);
            }

            baseData.Statuses = statusRepository.GetAll().ToList();
            return baseData;
        }

        public bool SaveSource(Source source)
        {
            if (source.Value == 0)
            {
                sourceRepository.Add(source);
            }
            else
            {
                sourceRepository.Update(source);
            }
            sourceRepository.SaveChanges();
            return true;
        }
    }
}
