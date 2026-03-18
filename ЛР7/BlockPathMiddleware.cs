namespace Backend_ЛР7_
{
    public class BlockPathMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
             
            if (context.Request.Path.StartsWithSegments("/blocked"))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return ;
            }

            await next(context);
        }
    }
}
