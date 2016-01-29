namespace FRS.MT940Loader.Fault
{
    public class MT940ValidationMessages
    {
        public const string HNF_HeaderSeparatorCannotBeNullOrEmpty = "The file 'header separator' argument cannot be null or empty.";
        public const string HNF_TrailerSeparatorCanotBeNullOrEmpty = "The file 'trailer separator' argument cannot be null or empty.";

        public const int FNF_C_FileNotFoundOnPath = 1;
        public const string FNF_FileNotFoundOnPath = "The file on the mentioned path was not found. Please provide a valid MT940 file path or make sure the MT940 file exists on the stated path.";

        public const int LFV_C_FileFailedLibraryValidationAndLoadToObject = 666;
        public const string LFV_FileFailedLibraryValidationAndLoadToObject = "The MT940 file has failed detailed validation and loading into an object.";

        public const int LFV_C_LibraryError = 667;
        public const string LFV_LibraryError = "Internal library error - *** {0} ***";
    }

    public class FRSLoadValidationFaults
    {
        public const int NRF_C_NoRecordFoundWithId = 1;
        public const string NRF_NoRecordFoundWithId = "There was no '{0}' record found in the database for the '{1}' - '{2}.'";

        public const int NRF_C_LinkedRecordNotFound = 2;
        public const string NRF_LinkedRecordNotFound = "A '{0}' record linked with a '{1}' record is required. There is surely an error in setup up of this '{1}' record.";
    }


}
