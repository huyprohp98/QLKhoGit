using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Resources
{
    public class InventoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int Amount { get; set; }
        public int StockId { get; set; }
        public string StockName { get; set; }

        public int UnitId { get; set; }
        public string UnitName { get; set; }
    }
}
