using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ECommerce.API.Model
{
    public class ApiCheckoutSummary
    {
        [JsonProperty("products")]
        public List<ApiCheckoutProduct> Products { get; set; }

        [JsonProperty("totalPrice")]
        public double TotalPrice { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutSummary"/> class.
        /// </summary>
        public ApiCheckoutSummary(CheckoutService.Model.CheckoutSummary summary)
        {
            this.TotalPrice = summary.TotalPrice;
            this.Date = summary.Date;
            Products = new List<ApiCheckoutProduct>(summary.Products.Select(p => new ApiCheckoutProduct(p)));
        }
    }
}
