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
    public class StocksController : ControllerBase
    {
        private readonly IStockRepositories _stockRepositories; /*ban chat la goi den services*/

        public StocksController(IStockRepositories stockRepositories)
        {
            _stockRepositories = stockRepositories;
        }

        [HttpGet]
        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
       
            var stock = await _stockRepositories.ListAsync();
            return stock;
        }
        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<Stock> paged = await _stockRepositories.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Stock resource)
        {
         
            var result = await _stockRepositories.SaveAsync(resource);

          
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _stockRepositories.DeleteAsync(id);

          
            return Ok(result);
        }
        [HttpDelete("DeleteWithName")]
        public async Task<IActionResult> DeleteWithName([FromBody] Stock resource)
        {
            var result = await _stockRepositories.DeleteWithName(resource.Name);

       
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Stock resource)
        {

            var result = await _stockRepositories.UpdateAsync(id, resource);


            return Ok(result);
        }
    }
}