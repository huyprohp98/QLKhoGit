using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLKho.Helper;
using QLKho.Models;
using QLKho.Repositories;

namespace DemoInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepositories _customerRepositories; /*ban chat la goi den services*/

        public CustomersController(ICustomerRepositories customerRepositories)
        {
            _customerRepositories = customerRepositories;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            //int a = 10;
            //int b = 11;
            //int c = a + b;
            var customer = await _customerRepositories.ListAsync();
            return customer;
        }
        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<Customer> paged = await _customerRepositories.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Customer resource)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState.GetErrorMessages());

            //var category = _mapper.Map<SaveCategoryResource, Category>(resource);
            var result = await _customerRepositories.SaveAsync(resource);

            //if (!result.Success)
            //    return BadRequest(result.Message);

            //var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _customerRepositories.DeleteAsync(id);

            //if (!result.Success)
            //    return BadRequest(result.Message);

            //var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            return Ok(result);
        }
        [HttpDelete("DeleteWithName")]
        public async Task<IActionResult> DeleteWithName([FromBody] Customer resource)
        {
            var result = await _customerRepositories.DeleteWithName(resource.Name);

            //if (!result.Success)
            //    return BadRequest(result.Message);

            //var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Customer resource)
        {

            var result = await _customerRepositories.UpdateAsync(id, resource);


            return Ok(result);
        }
    }
}