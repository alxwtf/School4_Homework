using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkApp
{
    class Discount
    {
        public string Name { get; set; }
        public bool ItPercent { get; set; }
        public int DiscountValue { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }

    }
}
