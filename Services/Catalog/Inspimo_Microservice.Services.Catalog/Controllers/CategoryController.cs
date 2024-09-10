using Inspimo_Microservice.Services.Catalog.Dtos;
using Inspimo_Microservice.Services.Catalog.Services.Abstract;
using Inspimo_Microservice.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inspimo_Microservice.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoroyService _categoroyService;

        public CategoryController(ICategoroyService categoroyService)
        {
            _categoroyService = categoroyService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _categoroyService.GetAllAsync();
            return CreateActionResultInstance(values);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryDto createCategoryDto)
        {
            var value = await _categoroyService.CreateAsync(createCategoryDto);
            return CreateActionResultInstance(value);
        }
    }
}
