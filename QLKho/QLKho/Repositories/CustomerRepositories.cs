
using DemoInventory.API.Models;
using Microsoft.EntityFrameworkCore;
using QLKho.Helper;
using QLKho.Models;
using QLKho.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoInventory.API.Repositories
{
    public class CustomerRepositories : BaseRepository, ICustomerRepositories
    {
        public CustomerRepositories(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await _context.Customer.ToListAsync();
        }
        public async Task<Customer> SaveAsync(Customer _obj)
        {
            await _context.Customer.AddAsync(_obj);
            await _context.SaveChangesAsync();
            return _obj;
        }
        public async Task<Customer> DeleteAsync(int id)
        {
            var _obj = await _context.Customer.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Customer.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Customer> DeleteWithName(string name)
        {
            var _obj = await _context.Customer.Where(o => o.Name == name).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Customer.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Customer> UpdateAsync(int id, Customer resource)
        {
            var _obj = await _context.Customer.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _obj.Name = resource.Name;
                _obj.Address = resource.Address;
                _obj.Phone = resource.Phone;
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<PagedList<Customer>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<Customer> _query = from u in _context.Customer
                                          orderby u.Name
                                       select new Customer { Id = u.Id, Name = u.Name, Address = u.Address,Phone=u.Phone };
            //_query = _query.
            //tìm kiếm
            if (pagingParams.SearchValue == "name")
            {
                if (string.IsNullOrEmpty(pagingParams.SearchKey) == false)
                {
                    // câu lệnh bên dưới là tìm kiếm bằng tuyệt đối
                    //_query = _query.Where(o => o.Name == pagingParams.SearchKey
                    // câu lệnh bên dưới là tìm kiếm gần bằng
                    _query = _query.Where(o => o.Name.Contains(pagingParams.SearchKey));
                }
            }
            

            //Sort sắp xếp
            if (pagingParams.SortKey == "name")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.Name);
                else
                    _query = _query.OrderByDescending(o => o.Name);

            }
            if (pagingParams.SortKey == "address")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.Address);
                else
                    _query = _query.OrderByDescending(o => o.Address);

            }
            if (pagingParams.SortKey == "phone")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.Phone);
                else
                    _query = _query.OrderByDescending(o => o.Phone);

            }
            return await PagedList<Customer>
                          .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
   
