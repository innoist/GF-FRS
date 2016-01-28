using FRS.MT940Loader.Services.InputOutput;
using System.ServiceModel;

namespace FRS.MT940Loader.Services
{
    [ServiceContract(Namespace = "http://www.gulffinance.com.sa/frs/v1/input")]
    public interface IFRSMT940LoaderWCFService
    {
        [OperationContract]
        ProcessMT940AfterInsertReturn ProcessMT940AfterInsert(ProcessMT940AfterInsertInput input);
    }
}
