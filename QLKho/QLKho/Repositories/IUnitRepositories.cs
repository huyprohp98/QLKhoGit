using QLKho.Helper;
using QLKho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Repositories
{
    public interface IUnitRepositories
    {
        Task<IEnumerable<Unit>> ListAsync();
        Task<PagedList<Unit>> GetAllPagingAsync(PagingParams pagingParams);
        Task<Unit> SaveAsync(Unit unit);
        Task<Unit> DeleteAsync(int id);

        Task<Unit> UpdateAsync(int id, Unit resource);
    }
}
