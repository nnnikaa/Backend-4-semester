using System.Net.Http.Headers;

namespace Backend_ЛР7_Воробьева_В.Д._241_333
{
    public class RequestTraceMiddleware:IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Guid TraceId = Guid.NewGuid();


            //context.Response.Headers["X-Trace-Id"] = TraceId.ToString();
            //context.Items["TraceId"] = context.Response.Headers["X-Trace-Id"];


            context.Response.Headers["X-Trace-Id"] = TraceId.ToString();
            context.Items["TraceId"] = TraceId.ToString();

            await next(context);
        }
    }
}



