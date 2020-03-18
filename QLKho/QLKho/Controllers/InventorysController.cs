using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLKho.Helper;
using QLKho.Models;
using QLKho.Repositories;
using QLKho.Resources;

namespace DemoInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventorysController : ControllerBase
    {
        private readonly IInventoryRepositories _inventoryRepositories; /*ban chat la goi den services*/

        public InventorysController(IInventoryRepositories inventoryRepositories)
        {
            _inventoryRepositories = inventoryRepositories;
        }

        [HttpGet]
        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
           
            var inventory = await _inventoryRepositories.ListAsync();
            return inventory;
        }
        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<InventoryViewModel> paged = await _inventoryRepositories.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Inventory resource)
        {
           
            var result = await _inventoryRepositories.SaveAsync(resource);

        
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _inventoryRepositories.DeleteAsync(id);

          
            return Ok(result);
        }
        [HttpDelete("DeleteWithName")]
        public async Task<IActionResult> DeleteWithName([FromBody] Inventory resource)
        {
            var result = await _inventoryRepositories.DeleteWithName(resource.Name);

        
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Inventory resource)
        {

            var result = await _inventoryRepositories.UpdateAsync(id, resource);


            return Ok(result);
        }
    }
}