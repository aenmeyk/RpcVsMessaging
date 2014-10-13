using System.Net;
using System.Net.Http;
using System.Web.Http;
using Enterprise.Purchasing.Persistence;

namespace Enterprise.Purchasing.WebApi.Controllers
{
    public class StockReplenishmentRequestsController : ApiController
    {
        private readonly PurchaseOrderStore _purchaseOrderStore;

        public StockReplenishmentRequestsController()
        {
            _purchaseOrderStore = new PurchaseOrderStore();
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody]string itemCode)
        {
            _purchaseOrderStore.SubmitPurchaseOrder(itemCode);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
