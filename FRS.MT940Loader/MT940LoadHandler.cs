using FRS.MT940Loader.Fault;
using System.Collections.Generic;
using System;

namespace FRS.MT940Loader
{
    public class MT940LoadHandler
    {
        private DatabaseHandler _dbHandler;
        private MT940Loader _mt940Loader;

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

        public MT940LoadHandler()
        {
            _dbHandler = new DatabaseHandler();
            _mt940Loader = new MT940Loader();
        }

        public List<MT940LoaderFault> ValidateLoadById(long id)
        {
            List<MT940LoaderFault> faults = new List<MT940LoaderFault>();
            Load load = _dbHandler.GetLoadById(id);

            //Validate the Load record
            if (load == null)
                faults.Add(new MT940LoaderFault(FRSLoadValidationFaults.NRF_C_NoRecordFoundWithId,
                                                  string.Format(FRSLoadValidationFaults.NRF_NoRecordFoundWithId, "Load", "LoadId", id.ToString())));
            if (load.LoadMetaData == null)
                faults.Add(new MT940LoaderFault(FRSLoadValidationFaults.NRF_C_LinkedRecordNotFound,
                                                  string.Format(FRSLoadValidationFaults.NRF_LinkedRecordNotFound, "LoadMetada", "Load")));

            if (load.MT940Load == null)
                faults.Add(new MT940LoaderFault(FRSLoadValidationFaults.NRF_C_LinkedRecordNotFound,
                                                  string.Format(FRSLoadValidationFaults.NRF_LinkedRecordNotFound, "MT940Load", "Load")));

            return faults.Count > 0 ? faults : null;
        }

        public void LoadMT940(long id)
        {
            MT940Load mt940Load = _dbHandler.GetMT940LoadById(id);            
        }
    }
}
