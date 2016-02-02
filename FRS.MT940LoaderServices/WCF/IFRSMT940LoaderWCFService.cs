using System.ServiceModel;
using FRS.MT940Loader.Services.InputOutput;

namespace FRS.MT940LoaderService.WCF
{
    [ServiceContract(Namespace = "http://www.gulffinance.com.sa/frs/v1/input")]
    public interface IFRSMT940LoaderWCFService
    {
        [OperationContract]
        ProcessMT940AfterInsertReturn LoadMT940AfterInsert(ProcessMT940AfterInsertInput input);
    }
}
