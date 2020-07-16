using ECommerce.ProductCatalog.Model;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.ProductCatalog
{
    class ServiceFabricProductRepository : IProductRepository
    {
        private readonly IReliableStateManager _stateManager;

        public ServiceFabricProductRepository(IReliableStateManager stateManager)
        {
            _stateManager = stateManager;
        }
        public async Task AddProduct(Product product)
        {
            IReliableDictionary<Guid, Product> products = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("products");

            using (ITransaction tx = _stateManager.CreateTransaction())
            {
                await products
                    .AddOrUpdateAsync(tx, product.Id, product, (id, value) => product);

                await tx.CommitAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            IReliableDictionary<Guid, Product> products = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("products");

            var result = new List<Product>();

            using(ITransaction tx = _stateManager.CreateTransaction())
            {
                Microsoft.ServiceFabric.Data.IAsyncEnumerable<KeyValuePair<Guid, Product>> allproducts = await products.CreateEnumerableAsync(tx, EnumerationMode.Unordered);

                using (Microsoft.ServiceFabric.Data.IAsyncEnumerator<KeyValuePair<Guid, Product>> enumerator = allproducts.GetAsyncEnumerator())
                {
                    while(await enumerator.MoveNextAsync(CancellationToken.None))
                    {
                        KeyValuePair<Guid, Product> current = enumerator.Current;
                        result.Add(current.Value);
                    }
                }
            }


            return result;
        }
    }
}
