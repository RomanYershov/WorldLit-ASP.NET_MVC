﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorldLib.Models
{
    public class Product
    {
        public int Id { get; set; }
        public double Cost { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; } 
        public string Description { get; set; }
        public virtual ISet<Ingridient> Ingridients { get; set; }
    }
}