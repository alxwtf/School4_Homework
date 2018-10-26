using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkApp
{
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public bool HaveDiscount { get; set; }
        public int DiscountValue { get; set; }
        public DateTime? StartSellDate { get; set; }
        public DateTime? EndSellDate { get; set; }
    }
}
