using System;

namespace OracleEntityFrameworkUnitTest.Classes
{
    public class Helpers
    {
        public static double KwHpConverter(double inputValue, string convertTo)
        {
            if (string.IsNullOrWhiteSpace(convertTo))
            {
                throw new ArgumentNullException($"{nameof(convertTo)} can not be null");
            }

            return string.Equals(convertTo, "hp", StringComparison.CurrentCultureIgnoreCase) ? 
                inputValue / 0.745699872 : 
                inputValue * 0.745699872;
        }
    }
}