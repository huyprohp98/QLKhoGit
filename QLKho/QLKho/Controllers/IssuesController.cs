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
    public class IssuesController : ControllerBase
    {
        private readonly IIssueRepositories _issueRepositories; /*ban chat la goi den services*/

        public IssuesController(IIssueRepositories issueRepositories)
        {
            _issueRepositories = issueRepositories;
        }

        [HttpGet]
        public async Task<IEnumerable<Issue>> GetAllAsync()
        {

            var issue = await _issueRepositories.ListAsync();
            return issue;
        }
        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<IssueViewModel> paged = await _issueRepositories.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Issue resource)
        {

            var result = await _issueRepositories.SaveAsync(resource);


            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _issueRepositories.DeleteAsync(id);


            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Issue resource)
        {

            var result = await _issueRepositories.UpdateAsync(id, resource);


            return Ok(result);
        }
    }
}