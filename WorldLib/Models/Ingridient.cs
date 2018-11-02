using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorldLib.Models
{
    public class Ingridient
    {
        public int Id { get; set; }
        public int ProductId { get; set; }  
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Cost { get; set; }    
        //[ForeignKey("ProductId")]
        //public Product Product { get; set; }
    }
}