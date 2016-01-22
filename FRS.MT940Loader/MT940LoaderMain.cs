using FRS.MT940Loader.Fault;
using Raptorious.SharpMt940Lib;
using Raptorious.SharpMt940Lib.Mt940Format;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FRS.MT940Loader
{
    public class MT940LoaderMain
    {
        private string _filename;
        private string _path;
        private string _headerSeperator;
        private string _trailerSeperator;
        
        public string Filename
        {
            get
            {
                return _filename;
            }

            set
            {
                _filename = value;
            }
        }

        public string FilePath
        {
            get
            {
                return _path;
            }

            set
            {
                _path = value;
            }
        }

        public List<FRSFileValidationFault> ValidationResults;

        public MT940LoaderMain(string filePath, string headerSeperator, string trailerSeperator)
        {
            if (string.IsNullOrEmpty(headerSeperator))
                throw new ArgumentException(MT940ValidationMessages.HNF_HeaderSeparatorCannotBeNullOrEmpty, "headerSeperator");

            if (string.IsNullOrEmpty(trailerSeperator))
                throw new ArgumentException(MT940ValidationMessages.HNF_TrailerSeparatorCanotBeNullOrEmpty, "trailerSeperator");

            if (ValidateFilePhysically(filePath))
            {
                _path = filePath;
                _filename = GetFileNameFromPath(filePath);
                _headerSeperator = headerSeperator;
                _trailerSeperator = trailerSeperator;

                ValidationResults = new List<FRSFileValidationFault>();

                return;
            }

            AddFilePhysicalValidatoinFaultAndThrowException();
        }

        private string GetFileNameFromPath(string path)
        {
            return Path.GetFileName(path);
        }

        private string GetFileNameWithoutExtensionFromPath(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        private bool ValidateFilePhysically(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Exists;
        }

        public bool ValidateFile()
        {
            return ValidateFile(_path);
        }

        private bool ValidateFile(string path)
        {
            if(!ValidateFilePhysically(path))
            {
                AddFilePhysicalValidationFault();
                return false;
            }

            try
            {
                var header = new Separator(_headerSeperator);
                var trailer = new Separator(_trailerSeperator);
                var genericFomat = new GenericFormat(header, trailer);
                var parsed = Mt940Parser.Parse(genericFomat, FilePath, CultureInfo.CurrentCulture);
            }
            catch(Exception ex)
            {
                AddFileLibraryInvalidation(ex);

                return false;
            }

            return true;
        }    
        
        #region Start - Exception and Fault Methods
        private void ClearList<T>(List<T> list)
        {
                list.Clear();
        }

        private void AddFilePhysicalValidatoinFaultAndThrowException()
        {
            ClearList(ValidationResults);
            AddFilePhysicalValidationFault();

            throw new FileNotFoundException(MT940ValidationMessages.FNF_FileNotFoundOnPath);
        }

        private void AddFilePhysicalValidationFault()
        {
            ClearList(ValidationResults);
            ValidationResults.Add(new FRSFileValidationFault(MT940ValidationMessages.FNF_C_FileNotFoundOnPath, MT940ValidationMessages.FNF_FileNotFoundOnPath));
        }

        private void AddFileLibraryInvalidation(Exception ex)
        {
            ClearList(ValidationResults);
            ValidationResults.Add(new FRSFileValidationFault(MT940ValidationMessages.LFV_C_FileFailedLibraryValidationAndLoadToObject,
                                                            MT940ValidationMessages.LFV_FileFailedLibraryValidationAndLoadToObject));
            string errorMessage = ex.Message;
            if (ex.InnerException != null && !String.IsNullOrEmpty(ex.InnerException.Message))
            {
                errorMessage += Environment.NewLine;
                errorMessage += "Error details: " + ex.InnerException.Message;
            }
            ValidationResults.Add(new FRSFileValidationFault(MT940ValidationMessages.LFV_C_LibraryError,
                                                            string.Format(MT940ValidationMessages.LFV_LibraryError, errorMessage)));
        }
        #endregion End - Exception and Fault Methods

    }
}
