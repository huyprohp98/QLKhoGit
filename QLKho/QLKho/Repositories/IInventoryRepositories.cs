using QLKho.Helper;
using QLKho.Models;
using QLKho.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Repositories
{
    public interface IInventoryRepositories
    {
        Task<IEnumerable<Inventory>> ListAsync();
        Task<PagedList<InventoryViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<Inventory> SaveAsync(Inventory inventory);
        Task<Inventory> DeleteAsync(int id);
        Task<Inventory> DeleteWithName(string name);
        Task<Inventory> UpdateAsync(int id, Inventory resource);
    }
}
