using QLKho.Helper;
using QLKho.Models;
using QLKho.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Repositories
{
   public interface IReceiptRepositories
    {
        Task<IEnumerable<Receipt>> ListAsync();
        Task<PagedList<ReceiptViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<Receipt> SaveAsync(Receipt receipt);
        Task<Receipt> DeleteAsync(int id);

        Task<Receipt> UpdateAsync(int id, Receipt resource);
    }
}
