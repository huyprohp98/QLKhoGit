using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int Amount { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public IList<Issue> Issue { get; set; } = new List<Issue>();
        public IList<Receipt> Receipt { get; set; } = new List<Receipt>();
    }
}
