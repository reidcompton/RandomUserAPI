using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NewClassrooms.Models
{

    [ExcludeFromCodeCoverage]
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Results { get; set; }
    }
}
