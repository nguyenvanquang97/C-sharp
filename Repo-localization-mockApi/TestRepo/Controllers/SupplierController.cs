using Library.Models;
using Library.Repositories.SupplierRepository;
using Library.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace TestRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository supplierRepository;

        public SupplierController(ISupplierRepository supplierRepository)
        {
            this.supplierRepository = supplierRepository;

        }
        [HttpGet]
        public async Task<ActionResult> getAllSupplier()
        {
            var suppliers = await supplierRepository.GetAll();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplierItem(int id)
        {
            var supplier = await supplierRepository.GetById(id);


            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplierItem(UpsertSupplierRequest request)
        {


            var supplier = await supplierRepository.Post(request);
            return Ok(supplier);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplierItem(int id, Supplier request)
        {

            var supplier = await supplierRepository.Put(id, request);

            return Ok(supplier);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierItem(int id)
        {
            var supplier = await supplierRepository.Delete(id);


            return Ok(supplier);
        }


    }
}
