using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;

namespace Ecom.Core.interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<bool> AddAsync(AddProductDTO ProductDTO);
        Task<bool> UpdateAsync( UpdateProductDTO ProductDTO);
        Task<bool> DeleteAsync(Product product);
    }
}
