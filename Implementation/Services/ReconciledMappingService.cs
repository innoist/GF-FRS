using System.Collections.Generic;
using System.Linq;
using FRS.Interfaces.IServices;
using FRS.Interfaces.Repository;
using FRS.Models.DomainModels;
using FRS.Models.RequestModels;
using FRS.Models.ResponseModels;
using FRS.Repository.Repositories;

namespace FRS.Implementation.Services
{
    public class ReconciledMappingService : IReconciledMappingService
    {
        #region Private

        private readonly IReconciledMappingRepository rcRepository;
        private readonly OracleGLEntryRepository oracleGlEntryRepository;
        private readonly MT940CustomerStatementTransactionRepository transactionRepository;
        //private readonly ILoadMetaDataRepository loadMetaDataRepository;

        #endregion

        #region Constructor

        public ReconciledMappingService(IReconciledMappingRepository mcRepository, OracleGLEntryRepository oracleGlEntryRepository, MT940CustomerStatementTransactionRepository transactionRepository)
        {
            this.rcRepository = mcRepository;
            this.oracleGlEntryRepository = oracleGlEntryRepository;
            this.transactionRepository = transactionRepository;
        }

        #endregion

        public IEnumerable<ReconciledMapping> GetAll()
        {
            return rcRepository.GetAll();
        }



        public bool SaveReconciledMapping(ReconciledMapping reconciledMapping)
        {
            rcRepository.Add(reconciledMapping);
            rcRepository.SaveChanges();
            return true;
        }

        public bool SaveReconciledMappings(IEnumerable<ReconciledMapping> reconciledMapping)
        {
            return rcRepository.SaveMappings(reconciledMapping);
        }

        public void DeleteReconciledMapping(long reconciledMappingId)
        {
            var reconciledMapping = rcRepository.Find(reconciledMappingId);
            if (reconciledMapping != null)
            {
                rcRepository.Delete(reconciledMapping);
                rcRepository.SaveChanges();
            }
        }

        public ReconciledMappingResponse GetReconciledMappingResponse(long ReconciledMappingId)
        {
            var mapping = rcRepository.Find(ReconciledMappingId);
            ReconciledMappingResponse response = new ReconciledMappingResponse
            {
                OracleGlEntry = mapping.OracleGLEntry,
                Transactions = rcRepository.GetReconciledMappings(mapping.OracleGLEntryId),
                ReconciledMapping = mapping
            };
            response.ReconciledMapping.TransactionAmount = response.Transactions.Sum(x => x.Amount);
            response.ReconciledMapping.TransactionsCount = response.Transactions.Count();

            return response;
        }

        public SearchTemplateResponse<ReconciledMapping> GetReconciledMappingSearchResponse(ReconciledMappingSearchRequest searchRequest)
        {
            return rcRepository.GetReconciledMappingSearchResponse(searchRequest);
        }
    }
}
