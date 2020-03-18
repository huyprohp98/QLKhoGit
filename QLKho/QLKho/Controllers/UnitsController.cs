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
    public class UnitsController : ControllerBase
    {
        private readonly IUnitRepositories _unitRepositories; /*ban chat la goi den services*/

        public UnitsController(IUnitRepositories unitRepositories)
        {
            _unitRepositories = unitRepositories;
        }

        [HttpGet]
        public async Task<IEnumerable<Unit>> GetAllAsync()
        {

            var unit = await _unitRepositories.ListAsync();
            return unit;
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<Unit> paged = await _unitRepositories.GetAllPagingAsync(pagingParams);
            //paged.tot
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged);

        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Unit resource)
        {
         
            var result = await _unitRepositories.SaveAsync(resource);

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _unitRepositories.DeleteAsync(id);

        
            return Ok(result);
        }
        [HttpDelete("DeleteWithName")]

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Unit resource)
        {

            var result = await _unitRepositories.UpdateAsync(id, resource);


            return Ok(result);
        }
    }
}