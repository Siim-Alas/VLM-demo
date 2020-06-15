using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VortexLatticeClassLibrary.Utilities
{
    public class GreaterThanValidationAttribute : ValidationAttribute
    {
        public GreaterThanValidationAttribute(double bound)
        {
            Bound = bound;
        }

        public double Bound { get; }

        public override bool IsValid(object value)
        {
            try
            {
                return Convert.ToDouble(value) > Bound;
            }
            catch
            {
                return false;
            }
        }
    }
}
