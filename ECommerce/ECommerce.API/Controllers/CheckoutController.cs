using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Model;
using ECommerce.CheckoutService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private static readonly Random rnd = new Random(DateTime.UtcNow.Second);

        [Route("{userId}")]
        public async Task<ApiCheckoutSummary> CheckoutAsync(string userId)
        {
            var summary = await GetCheckoutService().CheckoutAsync(userId);

            return new ApiCheckoutSummary(summary);
        }

        [Route("history/{userId}")]
        public async Task<IEnumerable<ApiCheckoutSummary>> GetHistoryAsync(string userId)
        {
            var history = await GetCheckoutService().GetOrderHistoryAsync(userId);

            return history.Select(h => new ApiCheckoutSummary(h));
        }


        private ICheckoutService GetCheckoutService()
        {
            return ServiceProxy.Create<ICheckoutService>(new Uri("fabric:/ECommerce/ECommerce.CheckoutService"), new ServicePartitionKey(1));
        }

    }
}
