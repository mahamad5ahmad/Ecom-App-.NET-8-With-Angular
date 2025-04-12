using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.interfaces
{
    public interface IUnitOfWork
    {
        public IPhotoRepository photoRepository { get; }
        public IProductRepository productRepository { get; }
        public ICategoryRepository categoryRepository { get; }
    }
}
