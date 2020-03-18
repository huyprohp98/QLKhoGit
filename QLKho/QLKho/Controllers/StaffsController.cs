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
    public class StaffsController : ControllerBase
    {
        private readonly IStaffRepositories _staffRepositories; /*ban chat la goi den services*/

        public StaffsController(IStaffRepositories staffRepositories)
        {
            _staffRepositories = staffRepositories;
        }

        [HttpGet]
        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            //int a = 10;
            //int b = 11;
            //int c = a + b;
            var staff = await _staffRepositories.ListAsync();
            return staff;
        }
        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<Staff> paged = await _staffRepositories.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Staff resource)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState.GetErrorMessages());

            //var category = _mapper.Map<SaveCategoryResource, Category>(resource);
            var result = await _staffRepositories.SaveAsync(resource);

            //if (!result.Success)
            //    return BadRequest(result.Message);

            //var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _staffRepositories.DeleteAsync(id);

            //if (!result.Success)
            //    return BadRequest(result.Message);

            //var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            return Ok(result);
        }
        [HttpDelete("DeleteWithName")]
        public async Task<IActionResult> DeleteWithName([FromBody] Staff resource)
        {
            var result = await _staffRepositories.DeleteWithName(resource.Name);

            //if (!result.Success)
            //    return BadRequest(result.Message);

            //var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Staff resource)
        {

            var result = await _staffRepositories.UpdateAsync(id, resource);


            return Ok(result);
        }
    
}
}