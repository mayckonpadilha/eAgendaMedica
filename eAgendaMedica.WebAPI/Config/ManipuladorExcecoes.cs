using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Text.Json;

namespace eAgenda.WebApi.Config
{
    public class ManipuladorExcecoes
    {
        private readonly RequestDelegate requestDelegate;
        public ManipuladorExcecoes(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                await requestDelegate.Invoke(context);
            }
           
            catch (Exception exc)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = " application/json";

                var formatedException = new
                {
                    Sucesso = false,
                    Erros = new List<string> { exc.Message }
                };



                context.Response.WriteAsync(JsonSerializer.Serialize(formatedException));
            }
        }
    }
}
