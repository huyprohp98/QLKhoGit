using QLKho.Helper;
using QLKho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Repositories
{
    public interface ICustomerRepositories
    {
        Task<PagedList<Customer>> GetAllPagingAsync(PagingParams pagingParams);
        Task<IEnumerable<Customer>> ListAsync();
        Task<Customer> SaveAsync(Customer customer);
        Task<Customer> DeleteAsync(int id);
        Task<Customer> DeleteWithName(string name);
        Task<Customer> UpdateAsync(int id, Customer resource);
    }
}
