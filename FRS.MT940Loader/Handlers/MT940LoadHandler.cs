﻿using System;
using FRS.MT940Loader.Faults;
using FRS.MT940Loader.Helpers;
using Raptorious.SharpMt940Lib;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace FRS.MT940Loader.Handlers
{
    public class MT940LoadHandler
    {
        private DatabaseHandler _dbHandler;
        private MT940Loader _mt940Loader;
        private short _AppConfigLoadTypeMT940Id = short.MinValue;

        internal DatabaseHandler DbHandler
        {
            get
            {
                return _dbHandler;
            }

            set
            {
                _dbHandler = value;
            }
        }

        internal MT940Loader Mt940Loader
        {
            get
            {
                return _mt940Loader;
            }

            set
            {
                _mt940Loader = value;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MT940LoadHandler()
        {
            _dbHandler = new DatabaseHandler();
            _mt940Loader = new MT940Loader();

            short.TryParse(DotNetHelper.ReadAppConfigAppSetting(LoaderConstants.RefDataLoadTypeMT940Id), out _AppConfigLoadTypeMT940Id);
        }

        #region **START - Private Methods **
        /// <summary>
        /// Internal validation method to check if the loads associated Load Metadata record's valid.
        /// </summary>
        /// <param name="loadMetadata">Load metadata object</param>
        /// <param name="faults">Faults collection</param>
        private void ValidateAssociatedLoadMetadataForNullOrEmpty(LoadMetaData loadMetadata, List<MT940LoaderFault> faults)
        {
            //Validate the associated Load Metadata
            if (loadMetadata == null)
            {
                faults.Add(new MT940LoaderFault(FRSLoadValidationFaults.LRNF_C_LinkedRecordNotFound,
                                                string.Format(FRSLoadValidationFaults.LRNF_LinkedRecordNotFound, "LoadMetada", "Load")));
            }
            else
            {
                if (loadMetadata.LoadType == null)
                {
                    faults.Add(new MT940LoaderFault(FRSLoadMetadataValidationFaults.NLTF_C_NoLoadTypeFound, FRSLoadMetadataValidationFaults.NLTF_NoLoadTypeFound));
                }
                else
                {
                    short metadataLoadTypeValue = DotNetHelper.ConvertShort(loadMetadata.LoadType.Value);
                    if (metadataLoadTypeValue != _AppConfigLoadTypeMT940Id)
                        faults.Add(new MT940LoaderFault(FRSLoadMetadataValidationFaults.RNM_C_RecordIsNotLoadTypeMT940,
                                                        string.Format(FRSLoadMetadataValidationFaults.RNM_RecordIsNotLoadTypeMT940, _AppConfigLoadTypeMT940Id.ToString(), metadataLoadTypeValue.ToString())));
                }
            }
        }

        /// <summary>
        /// Internal validation method to check if the loads associated MT940 Load record's valid.
        /// </summary>
        /// <param name="mt940Load">MT940 load object</param>
        /// <param name="faults">Fault collection</param>
        private void ValidateAssociatedMT940LoadForNullOrEmpty(MT940Load mt940Load, List<MT940LoaderFault> faults)
        {
            if (mt940Load == null)
            {
                faults.Add(new MT940LoaderFault(FRSLoadValidationFaults.LRNF_C_LinkedRecordNotFound,
                                                string.Format(FRSLoadValidationFaults.LRNF_LinkedRecordNotFound, "MT940Load", "Load")));
            }
            else
            {
                ValidateAssociatedFileContent(mt940Load.FileContent, faults);
            }
        }

        /// <summary>
        /// Internal validation method to check if the mt940 loads associated File Content's valid.
        /// </summary>
        /// <param name="fileContent">File content object</param>
        /// <param name="faults">Faults collection</param>
        private void ValidateAssociatedFileContent(FileContent fileContent, List<MT940LoaderFault> faults)
        {
            if (fileContent == null)
            {
                faults.Add(new MT940LoaderFault(FRSLoadValidationFaults.LRNF_C_LinkedRecordNotFound,
                                                string.Format(FRSLoadValidationFaults.LRNF_LinkedRecordNotFound, "FileContent", "MT940Load")));
            }
            else
            {
                if (string.IsNullOrEmpty(fileContent.FileContentBase64))
                    faults.Add(new MT940LoaderFault(FRSFileContentValidationFaults.NBD_C_NoBase64Data, FRSFileContentValidationFaults.NBD_NoBase64Data));
                //else if(!fileContent.FileContentBase64.IsBase64())
                //    faults.Add(new MT940LoaderFault(FRSFileContentValidationFaults.NBD_C_Base64DataNotValid, FRSFileContentValidationFaults.NBD_Base64DataNotValid));
            }

        }

        public void UpdateLoadForMT940LoadCompletion(Load updateLoadOnCompletion)
        {
            _dbHandler.DbContext.Loads.Attach(updateLoadOnCompletion);
            DbEntityEntry<Load> entry = _dbHandler.DbContext.Entry(updateLoadOnCompletion);
            entry.Property(e => e.Finish).IsModified = true;
            entry.Property(e => e.InProgress).IsModified = true;
            entry.Property(e => e.ModifiedBy).IsModified = true;            

            //Commit changes to database
            _dbHandler.DbContext.SaveChanges();
        }

        private MT940Balance AddMT940Balance(TransactionBalance transactionBalance)
        {
            Currency currency = (from c in _dbHandler.DbContext.Currencies
                                 where c.Name == transactionBalance.Currency.Code
                                 select c).FirstOrDefault();

            MT940Balance mt940Balance= _dbHandler.DbContext.MT940Balance.Create();
            
            return (mt940Balance = transactionBalance.ConvertTransactionBalanceToMT940Balance(currency.Value));
        }
        private MT940CustomerStatement AddMT940CustomerStatement(MT940CustomerStatement customerStatement)
        {            
            return _dbHandler.DbContext.MT940CustomerStatement.Add(customerStatement); ;
        }
        private long AddMT940CustomerStatementTransaction(MT940CustomerStatementTransaction customerStatementTransaction)
        {
            return _dbHandler.DbContext.MT940CustomerStatementTransaction.Add(customerStatementTransaction).MT940CustomerStatementTransactionId;
        }
        #endregion **END - Private Methods **

        #region **STRAT - Public Methods**
        /// <summary>
        /// Sets the header and trailer for the MT940 Load object
        /// </summary>
        /// <param name="headerSeperator">Header to be associated with this object</param>
        /// <param name="trailerSeperator">Trailer to be associated with this object</param>
        public void SetHeaderTrailer(string headerSeperator, string trailerSeperator)
        {
            _mt940Loader.HeaderSeperator = headerSeperator;
            _mt940Loader.TrailerSeperator = trailerSeperator;
        }

        /// <summary>
        /// Method to validate the Load id passed to process this MT940 load.
        /// </summary>
        /// <param name="id">Load record's primary key</param>
        /// <returns>Faults collection</returns>
        public List<MT940LoaderFault> ValidateLoadForNullOrEmpty(long id)
        {
            List<MT940LoaderFault> faults = new List<MT940LoaderFault>();
            Load load = _dbHandler.GetLoadById(id);

            //Validate the Load record
            //If no load record was returned
            if (load == null)
            {
                faults.Add(new MT940LoaderFault(FRSLoadValidationFaults.NRF_C_NoRecordFoundWithId,
                                                string.Format(FRSLoadValidationFaults.NRF_NoRecordFoundWithId, "Load", "LoadId", id.ToString())));
            }
            else
            {
                //Validate the associated Load Metadata record
                ValidateAssociatedLoadMetadataForNullOrEmpty(load.LoadMetaData, faults);

                //Validate the associated MT940Load
                ValidateAssociatedMT940LoadForNullOrEmpty(load.MT940Load, faults);
            }

            //Return null if there are no faults
            return faults.Count > 0 ? faults : null;
        }

        /// <summary>
        /// Method to check the base 64 contents of the MT940 files
        /// </summary>
        /// <param name="base64Content">Base64 string contents of MT940 file</param>
        /// <returns>Faults collection</returns>
        public List<MT940LoaderFault> ValidateMT940FileContent(string base64Content)
        {
            List<MT940LoaderFault> faults = new List<MT940LoaderFault>();

            _mt940Loader.ValidBase64MT940Content(base64Content);
                
            return faults;
        }

        /// <summary>
        /// Method to get the Load record from FRS database based on the passed id.
        /// </summary>
        /// <param name="id">Primary Key of Load record</param>
        /// <returns>Load object</returns>
        public Load GetLoad(long id)
        {
            return _dbHandler.GetLoadById(id);
        }

        /// <summary>
        /// Method to load the MT940 contents into the FRS database as per the schema.
        /// </summary>
        /// <param name="load">Load object for this load</param>
        /// <param name="base64Content">Base64 string contents of MT940 file</param>
        public void LoadMT940(Load load, string base64Content, string userId)
        {
            ICollection<CustomerStatementMessage> customerStatementMessages = _mt940Loader.LoadBase64MT940Content(base64Content);
            
            if (customerStatementMessages != null)
            {
                byte customerStatementSequence = 1;
                bool isSaveChanges = false;
                foreach (CustomerStatementMessage customerStatement in customerStatementMessages)
                {
                    MT940CustomerStatement mt940CustomerStatement = _dbHandler.DbContext.MT940CustomerStatement.Create();
                    
                    #region **START** Set common customer statement data
                    mt940CustomerStatement.MT940LoadId = Convert.ToInt64(load.MT940LoadId);
                    mt940CustomerStatement.Sequence = customerStatementSequence;
                    mt940CustomerStatement.ReadOnly = false;
                    mt940CustomerStatement.AccountNumber = customerStatement.Account;
                    mt940CustomerStatement.Description = customerStatement.Description;
                    mt940CustomerStatement.ReleatedMessage = customerStatement.RelatedMessage;
                    mt940CustomerStatement.SequenceNumber = customerStatement.SequenceNumber;
                    mt940CustomerStatement.StatementNumber = customerStatement.StatementNumber;
                    mt940CustomerStatement.TransactionReference = customerStatement.TransactionReference;
                    mt940CustomerStatement.TransactionCount = customerStatement.Transactions.Count;
                    mt940CustomerStatement.CreatedBy = userId;
                    mt940CustomerStatement.ModifiedBy = userId;

                    #region **START** Customer Statement Balances
                    if (customerStatement.ClosingAvailableBalance != null)
                    {
                        mt940CustomerStatement.ClosingAvailableBalance = AddMT940Balance(customerStatement.ClosingAvailableBalance);
                    }
                    if (customerStatement.ClosingBalance != null)
                    {
                        mt940CustomerStatement.ClosingBalance = AddMT940Balance(customerStatement.ClosingBalance);
                    }
                    if (customerStatement.ForwardAvailableBalance != null)
                    {
                        mt940CustomerStatement.ForwardAvailableBalance = AddMT940Balance(customerStatement.ForwardAvailableBalance);
                    }
                    if (customerStatement.OpeningBalance != null)
                    {
                        mt940CustomerStatement.OpeningBalance = AddMT940Balance(customerStatement.OpeningBalance);
                    }
                    #endregion **END** Customer Statement Balances

                    #endregion **END** Set common customer statement data

                    AddMT940CustomerStatement(mt940CustomerStatement);

                    #region **START** Customer Statement Transactions
                    byte transactionSequence = 1;
                    foreach (Transaction transaction in customerStatement.Transactions)
                    {
                        //Create a new MT940 Customer Statement Transaction
                        MT940CustomerStatementTransaction mt940CustomerStatementTransaction = new MT940CustomerStatementTransaction
                        {
                            MT940CustomerStatement = mt940CustomerStatement,
                            Sequence = transactionSequence,
                            ReadOnly = false,
                            Amount = transaction.Amount.Value,
                            DebitOrCredit = transaction.DebitCredit.ConvertToDebitOrCredit(),
                            Description = transaction.Description,
                            EntryDate = transaction.EntryDate,
                            FundsCode = transaction.FundsCode,
                            Reference = transaction.Reference,
                            TransactionType = transaction.TransactionType,
                            Value = transaction.Value,
                            ValueDate = transaction.ValueDate,
                            CreatedBy = userId,
                            ModifiedBy = userId,
                        };

                        //Associate this transaction with customer statement (master)
                        AddMT940CustomerStatementTransaction(mt940CustomerStatementTransaction);

                        //Increment the sequence for next transactions
                        transactionSequence++;
                    }
                    #endregion **END** Customer Statement Transactions

                    //Increment the sequence for next customer statement
                    customerStatementSequence++;

                    //Set the save flag to true, to be used for committing changes later on
                    isSaveChanges = true;
                }

                //If required commit changes to database
                if (isSaveChanges)
                {
                    _dbHandler.DbContext.SaveChanges();
                }
            }
        }

        

        #endregion **END - Public Methods**
    }
}
