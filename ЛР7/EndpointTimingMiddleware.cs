using System.Diagnostics;

namespace Backend_ЛР7_Воробьева_В.Д._241_333
{
    public class EndpointTimingMiddleware : IMiddleware
    {
        public async Task InvokeAsync ( HttpContext context, RequestDelegate next) 
        {
            var sw = Stopwatch.StartNew();

            context.Response.OnStarting(() =>
            {
                sw.Stop();



                context.Response.Headers["X-Endpoint-Elapsed-Ms"] = sw.Elapsed.TotalMilliseconds.ToString();
                return Task.CompletedTask;


            });

            await next(context);


        }
    }
}
