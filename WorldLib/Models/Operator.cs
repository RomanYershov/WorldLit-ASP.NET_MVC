using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldLib.Models
{
    public abstract class Operator
    {
      public abstract void Calculate();
    }

    public class ProductCalculate : Operator
    {
        public override void Calculate()
        {
            throw new NotImplementedException();
        }
    }
}