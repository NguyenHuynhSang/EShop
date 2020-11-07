using EShop.Server.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class Slide:Auditable
    {
        [Key]
        public int ID { set; get; }
        public string Image { set; get; }
        public string URL { set; get; }

        public bool IsActive { set; get; }

        public Slide()
        {
            IsActive = false;
       
        }

    }
}
