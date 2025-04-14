using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;

namespace Ecom.Infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IImageManagementService _imageManagementService;
        private readonly IMapper _mapper;
        public IPhotoRepository photoRepository { get; }

        public IProductRepository productRepository { get; }

        public ICategoryRepository categoryRepository { get; }


        public UnitOfWork(AppDbContext _context, IImageManagementService imageManagementService, IMapper mapper)
        {
            _imageManagementService = imageManagementService;
            _mapper = mapper;
            categoryRepository = new CategoryRepository(_context);
            productRepository = new ProductRepository(_context ,_mapper,_imageManagementService);
            photoRepository = new PhotoRepository(_context);

        }
    }
    
}

