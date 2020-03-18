using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public IList<Receipt> Receipt { get; set; } = new List<Receipt>();
    }
}
