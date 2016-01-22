namespace FRS.MT940Loader
{
    class MT940ValidationMessages
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
}
