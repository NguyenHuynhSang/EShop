using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Server.Entities
{
    [NotMapped]
    public class RevenueStatisticViewModelDateType
    {
        public DateTime Date { set; get; }
        public decimal Revenunes { set; get; }
        public decimal Benefis { set; get; }
    }
}
