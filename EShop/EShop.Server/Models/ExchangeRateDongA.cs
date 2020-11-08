using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class ExchangeRateDongA
    {
        [Key]
        public int Id { set; get; }
        public string type { get; set; }
        public string imageurl { get; set; }
        public float muatienmat { get; set; }
        public float muack { get; set; }
        public float bantienmat { get; set; }
        public float banck { get; set; }
    }
}
