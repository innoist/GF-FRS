using System;
using FRS.MT940Loader.Services.InputOutput;
using FRS.MT940Loader.Handlers;
using System.Collections.Generic;
using FRS.MT940Loader.Faults;
using System.ServiceModel;
using FRS.MT940Loader.Helpers;
using FRS.MT940Loader;

namespace FRS.MT940LoaderService.WCF
{
    public class FRSMT940LoaderWCFService : IFRSMT940LoaderWCFService
    {
        public ProcessMT940AfterInsertReturn LoadMT940AfterInsert(ProcessMT940AfterInsertInput input)
        {
            //Validate the input of this function call
            //Fetch all metadata of this function from the database
            //Call the Loader projects method to load into database and return the return object back
            //Make this call async and return the function after validation of input
            //Something like
            //MT940LoaderMain l = new MT940LoaderMain(@"C:\ISTWORK\CODE\GF.FRS\GF.FRS.MT940Loader\Samples\KSA\SCB Vostro - 031001548008 -940d.txt",
            //                                          "{1:F01AAALSARIAXXX.SN...ISN.}{2:I940SCBLGB20XWEBN}{3:{108:xxxxx}}{4:",
            //                                          "-}");
            //l.ValidateFile();

            MT940LoadHandler mt940LoadHandler = new MT940LoadHandler();

            //Validate the Load object fully, including the associated objects
            
            //List<MT940LoaderFault> faults = mt940LoadHandler.ValidateLoad(input.LoadId);
            //if(faults != null && faults.Count != 0)
            //{
            //    throw new FaultException(string.Format(" There was a fault validating passed Load Id = {0}. Fault details: {1}",
            //                                           input.LoadId.ToString(),
            //                                           DotNetHelper.WrapFaultListToString(faults)));
            //}

            //Validate the file data into a MT940 object
            Load load = mt940LoadHandler.GetLoad(input.LoadId);

            //Very important to set the header and trailer as these are going to be used later throughout this processing
            mt940LoadHandler.SetHeaderTrailer(load.LoadMetaData.Header, load.LoadMetaData.Footer);

            //Validate the MT940 Base64 contents
            //faults = mt940LoadHandler.ValidateMT940FileContent(load.MT940Load.FileContent.FileContentBase64);

            //Load the MT940 file data into objects and then to the database
            mt940LoadHandler.LoadMT940(load, load.MT940Load.FileContent.FileContentBase64);

            return null;
        }
    }
}
