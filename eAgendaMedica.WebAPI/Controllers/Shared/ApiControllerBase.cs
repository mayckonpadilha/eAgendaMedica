
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers.Shared
{
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult ProcessarResultado(Result result, object objectVm = null)
        {
            if (result.IsFailed)
                return BadRequest(result.Errors);

            return this.Ok(objectVm);
        }


        public override OkObjectResult? Ok(object? value)
        {
            return base.Ok(new
            {
                Sucesso = true,
                Dados = value
            });
        }

        public override NotFoundObjectResult? NotFound(object? value)
        {
            IList<IError> erros = (List<IError>)value;

            return base.NotFound(new
            {
                Sucesso = false,
                Erros = erros.Select(x => x.Message)
            });
        }

        public override BadRequestObjectResult BadRequest(object objetoComErros)
        {
            IList<IError> erros = (List<IError>)objetoComErros;

            return base.BadRequest(new
            {
                Sucesso = false,
                Erros = erros.Select(x => x.Message)
            });
        }
    }


    
}
