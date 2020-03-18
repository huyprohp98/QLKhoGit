using QLKho.Helper;
using QLKho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Repositories
{
    public interface IStaffRepositories
    {
        Task<PagedList<Staff>> GetAllPagingAsync(PagingParams pagingParams);
        Task<IEnumerable<Staff>> ListAsync();
        Task<Staff> SaveAsync(Staff customer);
        Task<Staff> DeleteAsync(int id);
        Task<Staff> DeleteWithName(string name);
        Task<Staff> UpdateAsync(int id, Staff resource);
    }
}
