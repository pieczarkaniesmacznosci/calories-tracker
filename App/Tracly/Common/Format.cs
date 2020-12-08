using System;

namespace Tracly.Common
{
    public class Format
    {
        public static string OneDecimal(double myNumber)
        {
            return string.Format("{0:0.0}", myNumber);
        }
        public static string DateOnly(DateTime date)
        {
            return string.Format("{0:MM/dd/yy}", date);
        }
    }
}