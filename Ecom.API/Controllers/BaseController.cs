using AutoMapper;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork work;
        protected readonly IMapper _mapper;
        public BaseController(IUnitOfWork work , IMapper mapper)
        {
            this.work = work;
            _mapper = mapper;
            // Constructor logic can be added here if needed
        }

    }
}
