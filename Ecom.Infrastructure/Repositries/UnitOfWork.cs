using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.interfaces;
using Ecom.Infrastructure.Data;

namespace Ecom.Infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IPhotoRepository photoRepository { get; }

        public IProductRepository productRepository { get; }

        public ICategoryRepository categoryRepository { get; }


        public UnitOfWork(AppDbContext _context)
        {
            categoryRepository = new CategoryRepository(_context);
            productRepository = new ProductRepository(_context);
            photoRepository = new PhotoRepository(_context);


        }
    }
    
}

