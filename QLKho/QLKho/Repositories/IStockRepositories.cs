using QLKho.Helper;
using QLKho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Repositories
{
    public interface IStockRepositories
    {
        Task<PagedList<Stock>> GetAllPagingAsync(PagingParams pagingParams);
        Task<IEnumerable<Stock>> ListAsync();
        Task<Stock> SaveAsync(Stock stock);
        Task<Stock> DeleteAsync(int id);
        Task<Stock> DeleteWithName(string name);
        Task<Stock> UpdateAsync(int id, Stock resource);
    }
}
