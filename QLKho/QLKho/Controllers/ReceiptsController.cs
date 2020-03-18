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
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptRepositories _receiptRepositories; /*ban chat la goi den services*/

        public ReceiptsController(IReceiptRepositories receiptRepositories)
        {
            _receiptRepositories = receiptRepositories;
        }

        [HttpGet]
        public async Task<IEnumerable<Receipt>> GetAllAsync()
        {

            var receipt = await _receiptRepositories.ListAsync();
            return receipt;
        }
        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<ReceiptViewModel> paged = await _receiptRepositories.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Receipt resource)
        {

            var result = await _receiptRepositories.SaveAsync(resource);


            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _receiptRepositories.DeleteAsync(id);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Receipt resource)
        {

            var result = await _receiptRepositories.UpdateAsync(id, resource);


            return Ok(result);
        }
    }

}