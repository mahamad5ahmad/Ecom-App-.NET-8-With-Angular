using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructure.Repositries
{
    public class ProductRepository : GenericRepository<Product> , IProductRepository

    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IImageManagementService _imageManagementService;

        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            _mapper = mapper;
            _context = context;
            _imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(AddProductDTO ProductDTO)
        {
            if(ProductDTO == null)
            {
                return false;
            }
            var product = _mapper.Map<Product>(ProductDTO);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            var pathes= await _imageManagementService.AddImageAsync(ProductDTO.Photos, ProductDTO.Name);
            var photos= pathes.Select(x => new Photo
            {
                ImageName = x,
                ProductId = product.Id
            }).ToList();
            await _context.Photos.AddRangeAsync(photos);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO is null)
            {
                return false;
            }
            var FindProduct = await _context.Products.Include(m => m.Category)
                .Include(m => m.Photos)
                .FirstOrDefaultAsync(m => m.Id == updateProductDTO.Id);

            if (FindProduct is null)
            {
                return false;
            }
            _mapper.Map(updateProductDTO, FindProduct);

            var FindPhoto = await _context.Photos.Where(m => m.ProductId == updateProductDTO.Id).ToListAsync();

            foreach (var item in FindPhoto)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _context.Photos.RemoveRange(FindPhoto);

            var ImagePath = await _imageManagementService.AddImageAsync(updateProductDTO.Photos, updateProductDTO.Name);

            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = updateProductDTO.Id,
            }).ToList();

            await _context.Photos.AddRangeAsync(photo);

            await _context.SaveChangesAsync();
            return true;

        }

        async Task<bool> IProductRepository.DeleteAsync(Product product)
        {
            var photos = await _context.Photos.Where(x => x.ProductId == product.Id).ToListAsync();
            foreach( var item in photos)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _context.Photos.RemoveRange(photos);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
