using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var categories = await work.categoryRepository.GetAllAsync();
                if (categories == null)
                {
                    return BadRequest(new ResponseAPI(400 ));
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> addCategory(CategoryDTO categorydto)
        {
            try
            {
                if (categorydto == null)
                {
                    return BadRequest("Category cannot be null");
                }
                var category = _mapper.Map<Category>(categorydto);

                await work.categoryRepository.AddAsync(category);
                return Ok(category);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-ID/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var category = await work.categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return BadRequest(new ResponseAPI(400)) ;
                }
                return Ok(category);

            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
                    }
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> update(CategoryDTOUpdate categorydtoupdate ) {
            try
            {
                if (categorydtoupdate == null) {
                    return BadRequest("Category cannot be null");
                }
                await work.categoryRepository.UpdateAsync(_mapper.Map<Category>(categorydtoupdate));
                return Ok(categorydtoupdate);

            }
            catch (Exception ex)
            {

                return BadRequest() ;
            }
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var category = await work.categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound("Category not found");
                }
                await work.categoryRepository.DeleteAsync(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-test-cateory")]
        public async Task<IActionResult> gettest()
        {
            return Ok();

        }
    }
}

