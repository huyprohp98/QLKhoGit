using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Models
{
    public class Issue
    {

        public int Id { set; get; }
        public string Name { set; get; }
        public DateTime Creatdate { set; get; }
        public int Amount { set; get; }
        public string Price { set; get; }
        public string Content { set; get; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
       
      
    }
     
}
