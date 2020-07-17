using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ECommerce.API.Model
{
    public class ApiCheckoutProduct
    {
        [JsonProperty("productId")]
        public Guid ProductId { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("quantity")]
        public int Quantity{ get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutProduct"/> class.
        /// </summary>
        public ApiCheckoutProduct(CheckoutService.Model.CheckoutProduct product)
        {
            this.ProductId = product.Product.Id;
            this.ProductName = product.Product.Name;
            this.Quantity = product.Quantity;
            this.Price = product.Price;
        }
    }
}
