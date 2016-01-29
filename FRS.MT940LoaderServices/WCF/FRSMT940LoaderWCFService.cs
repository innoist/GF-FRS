using System;
using FRS.MT940Loader;
using FRS.MT940Loader.Services;
using FRS.MT940Loader.Services.InputOutput;

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

            MT940LoadHandler loadHandler = new MT940LoadHandler();

            loadHandler.LoadMT940(input.MT940LoadId);

            throw new NotImplementedException();
        }
    }
}
