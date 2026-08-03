using System;
using System.Collections.Generic;

namespace NWCodeFirstMVC.Domain.Models
{
    public partial class OrderSubtotal
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
