﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKho.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Inventory> Inventory { get; set; } = new List<Inventory>();
      
    }
}
