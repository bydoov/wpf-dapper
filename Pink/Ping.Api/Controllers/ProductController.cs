using Microsoft.AspNetCore.Mvc;
using Ping.Api.Models;
using Ping.Domain.Services;
using System.Threading.Tasks;

namespace Ping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.Products.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.Products.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductRequest request)
        {
            var product = request.ToProductEntity();
            var data = await _unitOfWork.Products.Add(product);
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductRequest request)
        {
            var product = request.ToProductEntity(id);
            var data = await _unitOfWork.Products.Update(id, product);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delte([FromRoute] int id)
        {
            var data = await _unitOfWork.Products.Delete(id);
            return Ok(data);
        }
    }
}
