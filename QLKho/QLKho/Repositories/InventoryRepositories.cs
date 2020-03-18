
using DemoInventory.API.Models;
using Microsoft.EntityFrameworkCore;
using QLKho.Helper;
using QLKho.Models;
using QLKho.Repositories;
using QLKho.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoInventory.API.Repositories
{
    public class InventoryRepositories : BaseRepository, IInventoryRepositories
    {
        public InventoryRepositories(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Inventory>> ListAsync()
        {
            return await _context.Inventory.ToListAsync();
        }
        public async Task<Inventory> SaveAsync(Inventory _obj)
        {
            await _context.Inventory.AddAsync(_obj);
            await _context.SaveChangesAsync();
            return _obj;
        }
        public async Task<Inventory> DeleteAsync(int id)
        {
            var _obj = await _context.Inventory.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Inventory.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Inventory> DeleteWithName(string name)
        {
            var _obj = await _context.Inventory.Where(o => o.Name == name).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Inventory.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Inventory> UpdateAsync(int id, Inventory resource)
        {
            var _obj = await _context.Inventory.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _obj.Name = resource.Name;
                _obj.Price = resource.Price;
                _obj.Amount = resource.Amount;
                _obj.StockId = resource.StockId;
                _obj.UnitId = resource.UnitId;
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<PagedList<InventoryViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<InventoryViewModel> _query = from inv in _context.Inventory
                                                    join uni in _context.Unit on inv.UnitId equals uni.Id
                                                    join stock in _context.Stock on inv.StockId equals stock.Id
                                                    select new InventoryViewModel
                                                    {
                                                        Id = inv.Id,
                                                        Name = inv.Name,
                                                        Price = inv.Price,
                                                        Amount = inv.Amount,
                                                        UnitId = uni.Id,
                                                        UnitName = uni.Name,
                                                        StockId = stock.Id,
                                                        StockName = stock.Name
                                                    };
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
            if (pagingParams.SortKey == "price")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.Price);
                else
                    _query = _query.OrderByDescending(o => o.Price);
            }
            if (pagingParams.SortKey == "amount")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.Amount);
                else
                    _query = _query.OrderByDescending(o => o.Amount);
            }
            if (pagingParams.SortKey == "unitName")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.UnitName);
                else
                    _query = _query.OrderByDescending(o => o.UnitName);
            }
            if (pagingParams.SortKey == "stockName")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.StockName);
                else
                    _query = _query.OrderByDescending(o => o.StockName);
            }

            return await PagedList<InventoryViewModel>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);

        }
    }
}

