
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
    public class UnitRepositories : BaseRepository, IUnitRepositories
    {
        public UnitRepositories(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Unit>> ListAsync()
        {
            return await _context.Unit.ToListAsync();
        }
        public async Task<PagedList<Unit>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<Unit> _query = from u in _context.Unit
                                      orderby u.Name
                                      select new Unit { Id = u.Id, Name = u.Name };
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
            // tìm kiếm theo id
            //if (pagingParams.SearchValue == "id")
            //{
            //    if (string.IsNullOrEmpty(pagingParams.SearchKey) == false)
            //    {

            //        int _id = Convert.ToInt32(pagingParams.SearchKey);
            //        _query = _query.Where(o => o.Id == _id);
            //    }
            //}
            //Sort sắp xếp
            if (pagingParams.SortKey == "name")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.Name);
                else
                    _query = _query.OrderByDescending(o => o.Name);

            }
            return await PagedList<Unit>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);

        }

        public async Task<Unit> SaveAsync(Unit _obj)
        {
            await _context.Unit.AddAsync(_obj);
            await _context.SaveChangesAsync();
            return _obj;
        }
        public async Task<Unit> DeleteAsync(int id)
        {
            var _obj = await _context.Unit.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Unit.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }

        public async Task<Unit> DeleteWithName(string name)
        {
            var _obj = await _context.Unit.Where(o => o.Name == name).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Unit.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }

        public async Task<Unit> UpdateAsync(int id, Unit resource)
        {
            var _obj = await _context.Unit.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _obj.Name = resource.Name;

                await _context.SaveChangesAsync();
            }
            return _obj;
        }
    }
}
