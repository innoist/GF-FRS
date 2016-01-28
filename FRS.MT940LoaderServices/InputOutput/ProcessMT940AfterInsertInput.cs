using System.Runtime.Serialization;

namespace FRS.MT940Loader.Services.InputOutput
{
    [DataContract]
    public class ProcessMT940AfterInsertInput
    {
        [DataMember(Order = 0, IsRequired = true, Name = "LoadId")]
        public long MT940LoadId;

        [DataMember(Order = 0, IsRequired = true, Name = "MT940FileContents")]
        public string MT940FileContents;
    }
}
