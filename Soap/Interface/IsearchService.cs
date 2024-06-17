using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Soap.Interface
{
    [ServiceContract]
    public interface IsearchService
    {
        [OperationContract]

        Task<string> SearchEntitesAsyn(string search);
        
    }
    public class SearchServiceClient : ClientBase<IsearchService>, IsearchService
    {
        public SearchServiceClient(Binding binding, EndpointAddress address)
            : base(binding, address) { }

        public Task<string> SearchEntitesAsyn(string search)
        {
            return Channel.SearchEntitesAsyn(search);
        }

      
    }
}
