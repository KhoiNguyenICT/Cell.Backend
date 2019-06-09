using System;

namespace Cell.Core.Extensions
{
    public static class ManipulationExtension
    {
        public static int ToPercent(this int number, int total)
        {
            if (total == 0)
                return 0;

            var percent = (int)Math.Round((decimal)number / total * 100);

            return Math.Min(percent, 100);
        }
    }
}
