using AutoMapper;
using eAgenda.WebApi.Controllers.Shared;
using eAgendaMedica.Aplicacao.ModuloMedico;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebAPI.ViewModels.ModuloAtividade;
using eAgendaMedica.WebAPI.ViewModels.ModuloMedico;
using Microsoft.AspNetCore.Mvc;

namespace eAgendaMedica.WebAPI.Controllers
{
        [ApiController]
        [Route("api/medicos")]
        public class MedicoController : ApiControllerBase
        {

            private readonly ServicoMedico servicoMedico;
            private readonly IMapper mapeador;

            public MedicoController(ServicoMedico servicoMedico, IMapper mapeadorCategorias)
            {
                this.servicoMedico = servicoMedico;
                this.mapeador = mapeadorCategorias;
            }

            [HttpGet]

            [ProducesResponseType(typeof(List<ListarMedicoViewModel>), 200)]
            [ProducesResponseType(typeof(string[]), 404)]
            [ProducesResponseType(typeof(string[]), 500)]
            public async Task<IActionResult> SeleciontarTodos()
            {
                var medicoResult = await servicoMedico.SelecionarTodos();

                var viewModel = mapeador.Map<List<ListarMedicoViewModel>>(medicoResult.Value);

                return Ok(viewModel);
            }

            [HttpGet("selecionar-top-10-medicos")]

            [ProducesResponseType(typeof(List<ListarMedicoViewModel>), 200)]
            [ProducesResponseType(typeof(string[]), 404)]
            [ProducesResponseType(typeof(string[]), 500)]
            public async Task<IActionResult> SelecionarTop10Medicos(DateTime dataInicial,DateTime dataFinal)
            {
                var medicoResult = await servicoMedico.SelecionarTop10Medicos(dataInicial,dataFinal);

                var viewModel = mapeador.Map<List<ListarMedicoViewModel>>(medicoResult.Value);

                return Ok(viewModel);
            }

            [HttpGet("visualizacao-completa/{id}")]

            [ProducesResponseType(typeof(VisualizarMedicoViewModel), 200)]
            [ProducesResponseType(typeof(string[]), 404)]
            [ProducesResponseType(typeof(string[]), 500)]

            public async Task<IActionResult> SelecionarPorId(Guid id)
            {

                var result = await servicoMedico.SelecionarPorId(id);

                if (result.IsFailed)
                {
                    return NotFound(result.Errors.Select(x => x.Message));
                }

                return Ok(mapeador.Map<VisualizarMedicoViewModel>(result.Value));


            }


            [HttpPost]

            [ProducesResponseType(typeof(FormMedicoViewModel), 201)]
            [ProducesResponseType(typeof(string[]), 400)]
            [ProducesResponseType(typeof(string[]), 500)]
            public async Task<IActionResult> InserirMedico(FormMedicoViewModel medicoViewModel)
            {

                var medico = mapeador.Map<Medico>(medicoViewModel);

                var result = await servicoMedico.Inserir(medico);

                return ProcessarResultado(result.ToResult(), medicoViewModel);
            }

            [HttpPut("{id}")]

            [ProducesResponseType(typeof(FormMedicoViewModel), 200)]
            [ProducesResponseType(typeof(string[]), 400)]
            [ProducesResponseType(typeof(string[]), 404)]
            [ProducesResponseType(typeof(string[]), 500)]
            public async Task<IActionResult> EditarMedico(Guid id, FormMedicoViewModel medicoViewModel)
            {

                var result = await servicoMedico.SelecionarPorId(id);

                if (result.IsFailed)
                {
                    return NotFound(result.Errors.Select(x => x.Message));
                }


                var medicoAlterado = mapeador.Map(medicoViewModel, result.Value);

                var medicoResult = await servicoMedico.Editar(medicoAlterado);

                return ProcessarResultado(medicoResult.ToResult(), medicoViewModel);
            }

            [HttpDelete("{id}")]

            [ProducesResponseType(200)]
            [ProducesResponseType(typeof(string[]), 400)]
            [ProducesResponseType(typeof(string[]), 404)]
            [ProducesResponseType(typeof(string[]), 500)]
            public async Task<IActionResult> DeletarMedico(Guid id)
            {
                var result = await servicoMedico.SelecionarPorId(id);

                if (result.IsFailed)
                    return NotFound(result.Errors.Select(x => x.Message));

                return ProcessarResultado(servicoMedico.Excluir(result.Value));
            }





            [HttpPut("adicionar-uma-atividade/{id}")]

            [ProducesResponseType(typeof(FormMedicoViewModel), 200)]
            [ProducesResponseType(typeof(string[]), 400)]
            [ProducesResponseType(typeof(string[]), 404)]
            [ProducesResponseType(typeof(string[]), 500)]
            public async Task<IActionResult> AdicionarAtividadeAoMedico(Guid id, FormAtividadeViewModel atividadeViewModel)
            {

                var result = await servicoMedico.SelecionarPorId(id);

                if (result.IsFailed)
                {
                    return NotFound(result.Errors.Select(x => x.Message));
                }

                var atividade = new Atividade()
                {
                    Assunto = atividadeViewModel.Assunto,
                    DataRealizacao = atividadeViewModel.DataRealizacao,
                    HoraInicio = TimeSpan.Parse(atividadeViewModel.HoraInicio),
                    HoraTermino = TimeSpan.Parse(atividadeViewModel.HoraTermino),
                    TipoAtividadeEnum = atividadeViewModel.TipoAtividadeEnum,     
                };

                result.Value.AdicionarHorario(atividadeViewModel.DataRealizacao, TimeSpan.Parse(atividadeViewModel.HoraInicio) ,TimeSpan.Parse(atividadeViewModel.HoraTermino));

                atividade.AtribuirAtividade();

                var medicoResult = await servicoMedico.AdicionarAtividade(result.Value, atividade);

                return ProcessarResultado(medicoResult.ToResult());
            }
        }
    
}
