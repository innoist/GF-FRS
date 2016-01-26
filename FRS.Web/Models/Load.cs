using System;
using System.Web;

namespace FRS.Web.Models
{
    public class Load
    {
        public long LoadId { get; set; }
        public byte LoadMetaDataId { get; set; }
        public long? MT940LoadId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Finish { get; set; }
        public bool InProgress { get; set; }
        public bool ReadOnly { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string LoadTypeName { get; set; }
        public string MetaDataName { get; set; }
        public string MT940Detail { get; set; }
        public long LoadMetadataId { get; set; }
        public string Attachment { get; set; }
        public byte[] ImageUrlBytes
        {
            get
            {
                if (string.IsNullOrEmpty(Attachment))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = Attachment.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (Attachment.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = Attachment.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }
    }
}