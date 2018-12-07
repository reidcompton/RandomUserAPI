using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace NewClassrooms.Models
{

    [ExcludeFromCodeCoverage]
    public class Constants
    {
        public class Gender
        {
            public const string Male = "male";
            public const string Female = "female";
        }

        public class AlphabetRange
        {
            public const string AM = "A - M";
            public const string NZ = "N - Z";
        }

        public class AgeRange
        {
            public const string ZeroTwenty = "0 - 20";
            public const string TwentyForty = "21 - 40";
            public const string FortySixty = "41 - 60";
            public const string SixtyEighty = "61 - 80";
            public const string EightyOneHundred = "81 - 100";
            public const string OneHundredPlus = "101+";
        }

        public class ChartType
        {
            public const string Pie = "Pie";
            public const string Bar = "Bar";
            public const string Line = "Line";
        }
    }
}
