using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Mvc;
using Ecom.Core.DTO;
using System.Collections.Generic;
using Ecom.Core.Entities.Product;
namespace Ecom.API.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var products = await work.productRepository.
                    GetAllAsync(x => x.Category, x => x.Photos);
                if (products is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                // Correct syntax with variable name
                var result = _mapper.Map<List<ProductDTO>>(products);


                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400 , ex.Message));
            }

        }
        [HttpGet("get-by-ID/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var product = await work.productRepository.GetByIdAsync(id ,x=>x.Category , x=>x.Photos);
                if (product is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                var result = _mapper.Map<ProductDTO>(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400)  );
            }
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> addProduct(AddProductDTO productdto)
        {
            try
            {
                await work.productRepository.AddAsync(productdto);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400 , ex.Message)  );
            }
        }
        [HttpPut("update-product")]
        public async Task<IActionResult> updateProduct( UpdateProductDTO productdto )
        {
            try
            {
                await work.productRepository.UpdateAsync(productdto);
                return Ok(new ResponseAPI(200, "Updated Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var product=await work.productRepository.GetByIdAsync( id , x=>x.Category , x=>x.Photos );
                if (product is null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                var b= await work.productRepository.DeleteAsync(product);
                if (b)
                {
                    return Ok(new ResponseAPI(200, "Deleted Successfully"));
                }
                else
                {
                    return BadRequest(new ResponseAPI(400));
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400));
            }

        }
    }
}
