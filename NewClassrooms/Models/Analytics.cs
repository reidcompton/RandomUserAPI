using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using static NewClassrooms.Models.Constants;

namespace NewClassrooms.Models
{
    [ExcludeFromCodeCoverage]
    public class Analytics
    {
        public string ChartType { get; set; }
        public string Title { get; set; }
        public DataPoint[] Data { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class DataPoint
    {
        public string Label { get; set; }
        public decimal Statistic { get; set; }
    }
}
