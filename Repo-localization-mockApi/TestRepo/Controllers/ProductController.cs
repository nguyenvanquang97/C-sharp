using Library;
using Library.Models;
using Library.Repositories.ProductRepo;
using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TestRepo.Resources;

namespace TestRepo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IStringLocalizer<SharedResource> stringLocalizer;
        public ProductController(IProductRepository productRepository,IStringLocalizer<SharedResource> stringLocalizer)
        {
            _productRepository = productRepository;
            this.stringLocalizer = stringLocalizer;
        }
        [HttpGet]
        public async Task<ActionResult> getAllProduct()
        {
            var products =await _productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductItem(int id)
        {
           
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                var dos = stringLocalizer["Not found"];
                return Ok(dos.Value);
            }
            
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProductItem(UpsertProductRequest request)
        {

           
            var product =await _productRepository.Post(request);
            return Ok(product);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductItem(int id,UpsertProductRequest request)
        {

            var product = await _productRepository.Put(id, request);
            if (product == null)
            {
                var dos = stringLocalizer["Not found"];
                return Ok(dos.Value);
            }
            return Ok(product);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductItem(int id)
        {
            var product = await _productRepository.Delete(id);
            if (product == null)
            {
                var dos = stringLocalizer["Not found with id ="+id];
                return Ok(dos.Value);
            }

            return Ok(product);
        }


    }
}
