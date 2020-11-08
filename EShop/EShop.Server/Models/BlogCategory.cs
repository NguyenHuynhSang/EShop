﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Server.Models
{
    public class BlogCategory
    {
        [Key]
       
        public int Id { set; get; }

        public string Name { set; get; }
    }
}
