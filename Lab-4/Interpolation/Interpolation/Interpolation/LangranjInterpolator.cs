namespace Interpolation
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class LangranjInterpolator : CommonInterpolator
    {
        public LangranjInterpolator(double[] values)
            : base(values)
        {
        }

        public override double CalculateValue(double x)
        {
            if (x < 0)
            {
                return Values[0];
            }

            double langrangePol = 0;

            for (int i = 0; i < Values.Length; i++)
            {
                double basicsPol = 1;
                for (int j = 0; j < Values.Length; j++)
                {
                    if (j != i)
                    {
                        basicsPol *= (x - j) / (i - j);
                    }
                }

                langrangePol += basicsPol * Values[i];
            }

            return langrangePol;
        }
    }
}
