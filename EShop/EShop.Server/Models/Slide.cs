using EShop.Server.Interface;
using EShop.Server.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models
{
    public class Slide : IAuditAble, IActiveAble
    {
        [Key]
        public int Id { set; get; }
        public string Image { set; get; }
        public string URL { set; get; }

        public bool IsActive { set; get; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get  ; set  ; }


        public int SlideGroupId { set; get; }
        [ForeignKey("SlideGroupId")]
        public SlideGroup SlideGroup { set; get; }

        public Slide()
        {
            IsActive = false;

        }

    }
}
