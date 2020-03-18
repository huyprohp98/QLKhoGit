using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Resources
{
    public class IssueViewModel
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public DateTime Creatdate { get; set; }
        public int Amount { set; get; }
        public string Price { set; get; }
        public string Content { set; get; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int InventoryId { get; set; }
        public string InventoryName { get; set; }
      
     
    }
}
