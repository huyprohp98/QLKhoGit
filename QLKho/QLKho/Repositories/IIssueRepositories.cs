using QLKho.Helper;
using QLKho.Models;
using QLKho.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Repositories
{
   public interface IIssueRepositories
    {
        Task<IEnumerable<Issue>> ListAsync();
        Task<PagedList<IssueViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<Issue> SaveAsync(Issue issue);
        Task<Issue> DeleteAsync(int id);
  
        Task<Issue> UpdateAsync(int id, Issue resource);
    }
}
