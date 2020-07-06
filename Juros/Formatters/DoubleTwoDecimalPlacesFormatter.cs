using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Juros.Formatters
{
    public class DoubleTwoDecimalPlacesFormatter : OutputFormatter
    {

        public DoubleTwoDecimalPlacesFormatter()
        {
            SupportedMediaTypes.Add("application/json");
        }
        protected override bool CanWriteType(Type type) => type == typeof(double);

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;
            var value = (double)context.Object;
            var formatted = value.ToString("f2", CultureInfo.InvariantCulture);


            response.WriteAsync(formatted);

            return Task.FromResult(0);
        }
    }
}