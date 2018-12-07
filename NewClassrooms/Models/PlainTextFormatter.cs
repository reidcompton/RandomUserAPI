using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace NewClassrooms.Models
{
    [ExcludeFromCodeCoverage]
    public class PlainTextFormatter : TextOutputFormatter
    {
            public PlainTextFormatter()
            {
                SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));
                SupportedEncodings.Add(Encoding.UTF8);
                SupportedEncodings.Add(Encoding.Unicode);
            }

            protected override bool CanWriteType(Type type)
            {
                if (typeof(Analytics).IsAssignableFrom(type) || typeof(IEnumerable<Analytics>).IsAssignableFrom(type))
                {
                    return base.CanWriteType(type);
                }

                return false;
            }

            public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
            {
                var response = context.HttpContext.Response;
                var buffer = new StringBuilder();

                if (context.Object is IEnumerable<Analytics>)
                {
                    foreach (var analytics in (IEnumerable<Analytics>)context.Object)
                    {
                        FormatPlainText(buffer, analytics);
                    }
                }
                else
                {
                    FormatPlainText(buffer, (Analytics)context.Object);
                }

                using (var writer = context.WriterFactory(response.Body, selectedEncoding))
                {
                    return writer.WriteAsync(buffer.ToString());
                }
            }

            private static void FormatPlainText(StringBuilder buffer, Analytics analytics)
            {
                
                buffer.AppendLine($"{analytics.Title}: {string.Join(", ", analytics.Data.Select(y => $"{y.Label}: {y.Statistic}%"))}");
            }
    }
}
