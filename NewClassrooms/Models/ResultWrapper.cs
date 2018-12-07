using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace NewClassrooms.Models
{
    [ExcludeFromCodeCoverage]
    // class to wrap the result from randomperson.me
    public class ResultWrapper
    {
        public Person[] Results { get; set; }
        public Info Info { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Info
    {
        public string Seed { get; set; }
        public int Results { get; set; }
        public int Page { get; set; }
        public string Version { get; set; }
    }
}
