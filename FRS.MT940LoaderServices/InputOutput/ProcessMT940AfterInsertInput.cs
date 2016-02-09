using System.Runtime.Serialization;

namespace FRS.MT940Loader.Services.InputOutput
{
    [DataContract]
    public class ProcessMT940AfterInsertInput
    {
        [DataMember(Order = 0, IsRequired = true, Name = "LoadId")]
        public long LoadId;
        //NADIR:
        [DataMember(Order = 1, IsRequired = true, Name = "UserId")]
        public string UserId;
    }
}
