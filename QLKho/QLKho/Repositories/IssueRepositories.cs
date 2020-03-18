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
    public class IssueRepositories : BaseRepository, IIssueRepositories
    {
        public IssueRepositories(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Issue>> ListAsync()
        {
            return await _context.Issue.ToListAsync();
        }
        public async Task<Issue> SaveAsync(Issue _obj)
        {
            await _context.Issue.AddAsync(_obj);
            await _context.SaveChangesAsync();
            return _obj;
        }
        public async Task<Issue> DeleteAsync(int id)
        {
            var _obj = await _context.Issue.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Issue.Remove(_obj);
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
       
        public async Task<Issue> UpdateAsync(int id, Issue resource)
        {
            var _obj = await _context.Issue.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _obj.Name = resource.Name;
                _obj.Creatdate = resource.Creatdate;
                _obj.Amount = resource.Amount;
                _obj.Price = resource.Price;
                _obj.Content = resource.Content;
                _obj.CustomerId = resource.CustomerId;
                _obj.InventoryId = resource.InventoryId;
               
             
                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<PagedList<IssueViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<IssueViewModel> _query = from iss in _context.Issue
                                                   
                                                   
                                                    join cus in _context.Customer on iss.CustomerId equals cus.Id
                                                    join inv in _context.Inventory on iss.InventoryId equals inv.Id
                                                select new IssueViewModel
                                                    {
                                                        Id = iss.Id,
                                                        Name = iss.Name,
                                                        Creatdate = iss.Creatdate,
                                                        Amount = iss.Amount,
                                                        Price = iss.Price,
                                                        Content =iss.Content,
                                                        CustomerId = cus.Id,
                                                        CustomerName =cus.Name,
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
            if (pagingParams.SortKey == "customerName")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.CustomerName);
                else
                    _query = _query.OrderByDescending(o => o.CustomerName);
            }
            //Sort sắp xếp 7
            if (pagingParams.SortKey == "inventoryName")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.InventoryName);
                else
                    _query = _query.OrderByDescending(o => o.InventoryName);
            }
           
          

            return await PagedList<IssueViewModel>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);

        }
    }
}
