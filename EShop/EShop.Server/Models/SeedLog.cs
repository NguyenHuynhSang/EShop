using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    [Table("SeedLog")]
    public class SeedLog
    {
        [Key] // chỉ định    khóa chính
        public int Id { set; get; }
        public int DataVersion { set; get; }

    }
}
