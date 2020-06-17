using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SvarnyJunak.CeskeObce.Web.Middlewares.ApplicationInsights
{
    public class NotFoundFilter : ITelemetryProcessor
    {
        private ITelemetryProcessor _next;

        public NotFoundFilter(ITelemetryProcessor next)
        {
            _next = next;
        }

        public void Process(ITelemetry item)
        {
            if (item is RequestTelemetry request && HasHttpStatusCode(request, HttpStatusCode.NotFound))
            {
                return;
            }

            _next.Process(item);
        }

        private static bool HasHttpStatusCode(RequestTelemetry request, HttpStatusCode httpStatus)
        {
            var httpStatusCode = ((int)httpStatus).ToString();
            return request.ResponseCode.Equals(httpStatusCode, StringComparison.OrdinalIgnoreCase);
        }
    }
}
