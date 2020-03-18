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
    public class StaffRepositories : BaseRepository, IStaffRepositories
    {
        public StaffRepositories(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Staff>> ListAsync()
        {
            return await _context.Staff.ToListAsync();
        }
        public async Task<Staff> SaveAsync(Staff _obj)
        {
            await _context.Staff.AddAsync(_obj);
            await _context.SaveChangesAsync();
            return _obj;
        }
        public async Task<Staff> DeleteAsync(int id)
        {
            var _obj = await _context.Staff.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Staff.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Staff> DeleteWithName(string name)
        {
            var _obj = await _context.Staff.Where(o => o.Name == name).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Staff.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Staff> UpdateAsync(int id, Staff resource)
        {
            var _obj = await _context.Staff.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _obj.Name = resource.Name;
                _obj.Address = resource.Address;
                _obj.Phone = resource.Phone;
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<PagedList<Staff>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<Staff> _query = from u in _context.Staff
                                       orderby u.Name
                                          select new Staff { Id = u.Id, Name = u.Name, Address = u.Address, Phone = u.Phone };
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
            return await PagedList<Staff>
                          .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
