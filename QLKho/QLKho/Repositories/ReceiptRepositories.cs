using DemoInventory.API.Models;
using Microsoft.EntityFrameworkCore;
using QLKho.Helper;
using QLKho.Models;
using QLKho.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Repositories
{
    public class ReceiptRepositories : BaseRepository, IReceiptRepositories
    {
        public ReceiptRepositories(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Receipt>> ListAsync()
        {
            return await _context.Receipt.ToListAsync();
        }
        public async Task<Receipt> SaveAsync(Receipt _obj)
        {
            await _context.Receipt.AddAsync(_obj);
            await _context.SaveChangesAsync();
            return _obj;
        }
        public async Task<Receipt> DeleteAsync(int id)
        {
            var _obj = await _context.Receipt.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Receipt.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }

        public async Task<Receipt> UpdateAsync(int id, Receipt resource)
        {
            var _obj = await _context.Receipt.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _obj.Name = resource.Name;
                _obj.Creatdate = resource.Creatdate;
                _obj.Amount = resource.Amount;
                _obj.Price = resource.Price;
                _obj.Content = resource.Content;
                _obj.StaffId = resource.StaffId;
                _obj.InventoryId = resource.InventoryId;


                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<PagedList<ReceiptViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<ReceiptViewModel> _query = from iss in _context.Receipt


                                                  join st in _context.Staff on iss.StaffId equals st.Id
                                                join inv in _context.Inventory on iss.InventoryId equals inv.Id
                                                select new ReceiptViewModel
                                                {
                                                    Id = iss.Id,
                                                    Name = iss.Name,
                                                    Creatdate = iss.Creatdate,
                                                    Amount = iss.Amount,
                                                    Price = iss.Price,
                                                    Content = iss.Content,
                                                    StaffId = st.Id,
                                                    StaffName = st.Name,
                                                    InventoryId = inv.Id,
                                                    InventoryName = inv.Name,


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
            //Sort sắp xếp 2
            if (pagingParams.SortKey == "creatdate")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.Creatdate);
                else
                    _query = _query.OrderByDescending(o => o.Creatdate);
            }
            //Sort sắp xếp 3
            if (pagingParams.SortKey == "amount")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.Amount);
                else
                    _query = _query.OrderByDescending(o => o.Amount);
            }
            //Sort sắp xếp 4
            if (pagingParams.SortKey == "price")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.Price);
                else
                    _query = _query.OrderByDescending(o => o.Price);
            }
            //Sort sắp xếp 5
            if (pagingParams.SortKey == "content")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.Content);
                else
                    _query = _query.OrderByDescending(o => o.Content);
            }
            //Sort sắp xếp 6
            if (pagingParams.SortKey == "staffName")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.StaffName);
                else
                    _query = _query.OrderByDescending(o => o.StaffName);
            }
            //Sort sắp xếp 7
            if (pagingParams.SortKey == "inventoryName")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.InventoryName);
                else
                    _query = _query.OrderByDescending(o => o.InventoryName);
            }



            return await PagedList<ReceiptViewModel>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);

        }
    }
}
