
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
    public class StockRepositories : BaseRepository, IStockRepositories
    {
        public StockRepositories(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Stock>> ListAsync()
        {
            return await _context.Stock.ToListAsync();
        }
        public async Task<Stock> SaveAsync(Stock _obj)
        {
            await _context.Stock.AddAsync(_obj);
            await _context.SaveChangesAsync();
            return _obj;
        }
        public async Task<Stock> DeleteAsync(int id)
        {
            var _obj = await _context.Stock.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Stock.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Stock> DeleteWithName(string name)
        {
            var _obj = await _context.Stock.Where(o => o.Name == name).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Stock.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Stock> UpdateAsync(int id, Stock resource)
        {
            var _obj = await _context.Stock.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _obj.Name = resource.Name;
                _obj.Address = resource.Address;
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<PagedList<Stock>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<Stock> _query = from u in _context.Stock
                                       orderby u.Name
                                       select new Stock { Id = u.Id, Name = u.Name, Address = u.Address };
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
            if (pagingParams.SearchValue == "address")
            {
                if (string.IsNullOrEmpty(pagingParams.SearchKey) == false)
                {
                    // câu lệnh bên dưới là tìm kiếm bằng tuyệt đối
                    //_query = _query.Where(o => o.Name == pagingParams.SearchKey
                    // câu lệnh bên dưới là tìm kiếm gần bằng
                    _query = _query.Where(o => o.Address.Contains(pagingParams.SearchKey));
                }
            }
            //Tìm kiếm tất cả
            //if (string.IsNullOrEmpty(pagingParams.Keyword) == false)
            //{
            //    _query = _query.Where(o => o.Name.Contains(pagingParams.Keyword) ||
            //    o.Name.Contains(pagingParams.Keyword));
            //    _query = _query.Where(o => o.Address.Contains(pagingParams.Keyword) ||
            //    o.Address.Contains(pagingParams.Keyword));

            //}

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
            return await PagedList<Stock>
                          .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
