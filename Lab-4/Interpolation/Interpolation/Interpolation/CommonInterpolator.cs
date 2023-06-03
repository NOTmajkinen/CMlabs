namespace Interpolation
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class CommonInterpolator
    {
        private double[] _values;

        public CommonInterpolator(double[] values)
        {
            _values = values;
        }

        protected double[] Values => _values;

        public virtual double CalculateValue(double x)
        {
            return 0;
        }
    }
}
