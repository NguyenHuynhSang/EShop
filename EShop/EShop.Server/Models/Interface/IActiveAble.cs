using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Models.Interface
{
    public interface IActiveAble
    {
        public Boolean IsActive { set; get; }
    }
}
