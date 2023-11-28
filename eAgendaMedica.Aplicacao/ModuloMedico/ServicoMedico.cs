using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio;
using eAgendaMedica.Dominio.ModuloMedico;
using FluentResults;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaMedica.Aplicacao.ModuloMedico
{
    public class ServicoMedico : ServicoBase<Medico, ValidadorMedico>
    {
        private IRepositorioMedico repositorioMedico;
        private IContextoPersistencia contextoPersistencia;

        public ServicoMedico(IRepositorioMedico repositorioMedico,
                             IContextoPersistencia contextoPersistencia)
        {
            this.repositorioMedico = repositorioMedico;
            this.contextoPersistencia = contextoPersistencia;
        }

        public async Task<Result<Medico>> Inserir(Medico medico)
        {
            Log.Logger.Debug("Tentando inserir medico... {@c}", medico);

            Result resultado = Validar(medico);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioMedico.Inserir(medico);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("medico {medicoId} inserido com sucesso", medico.Id);

                return Result.Ok(medico);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar inserir o medico";

                Log.Logger.Error(ex, msgErro + " {medicoId} ", medico.Id);

                throw new Exception(msgErro, ex);
            }
        }

        public async Task<Result<Medico>> Editar(Medico medico)
        {
            Log.Logger.Debug("Tentando editar medico... {@c}", medico);

            var resultado = Validar(medico);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioMedico.Editar(medico);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("medico {medicoId} editado com sucesso", medico.Id);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar editar o medico";

                Log.Logger.Error(ex, msgErro + " {medicoId}", medico.Id);

                throw new Exception(msgErro, ex);
            }

            return Result.Ok(medico);
        }

        public async Task<Result> Excluir(Guid id)
        {
            var medicoResult = await SelecionarPorId(id);

            if (medicoResult.IsSuccess)
                return Excluir(medicoResult.Value);

            return Result.Fail(medicoResult.Errors);
        }

        public Result Excluir(Medico medico)
        {
            Log.Logger.Debug("Tentando excluir medico... {@c}", medico);

            try
            {
                repositorioMedico.Excluir(medico);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("medico {medicoId} editado com sucesso", medico.Id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar excluir o medico";

                Log.Logger.Error(ex, msgErro + " {medicoId}", medico.Id);

                throw new Exception(msgErro, ex);
            }
        }

        public async Task<Result<List<Medico>>> SelecionarTodos()
        {
            Log.Logger.Debug("Tentando selecionar medicos...");

            try
            {
                var medicos = repositorioMedico.SelecionarTodos();

                medicos.ForEach(m => m.VerificarSeEstahEmAtividade());
                medicos.ForEach(m => m.VerificarDescanso());

                Log.Logger.Information("medicos selecionados com sucesso");

                return Result.Ok(medicos);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todos os medicos";

                Log.Logger.Error(ex, msgErro);

                throw new Exception(msgErro, ex);
            }
        }

        public async Task<Result<Medico>> SelecionarPorId(Guid id)
        {
            Log.Logger.Debug("Tentando selecionar medico {medicoId}...", id);

            try
            {
                var medico = repositorioMedico.SelecionarPorId(id);

                medico.VerificarSeEstahEmAtividade();
                medico.VerificarDescanso();

                if (medico == null)
                {
                    Log.Logger.Warning("medico {medicoId} não encontrado", id);

                    return Result.Fail("medico não encontrado");
                }

                Log.Logger.Information("medico {medicoId} selecionado com sucesso", id);

                return Result.Ok(medico);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar o medico";

                Log.Logger.Error(ex, msgErro + " {medicoId}", id);

                throw new Exception(msgErro, ex);
            }
        }

        public async Task<Result<Medico>> AdicionarAtividade(Medico medico,Atividade atividade)
        {
            Log.Logger.Debug("Tentando adicionar atividade ao medico {MedicoId}...", medico.Id);

            try
            {
                medico.AdicionarAtividade(atividade);

                repositorioMedico.Editar(medico);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Medico {MedicoId} teve a atividade com sucesso", medico.Id);

                return Result.Ok(medico);
            }
            catch (Exception exc)
            {
                string msgErro = "Falha no sistema ao tentar adicionar atividade ao Medico";

                Log.Logger.Error(exc, msgErro + " {MedicoId}", medico.Id);

                throw new Exception(msgErro, exc);
            }
        }

        public async Task<Result<List<Medico>>> SelecionarTop10Medicos(DateTime dataInicial, DateTime dataFinal)
        {
            Log.Logger.Debug("Tentando selecionar top 10 medicos...");

            try
            {
                var medicos = repositorioMedico.SelecionarTop10MedicosMaisTrabalhadores(dataInicial,dataFinal);

                medicos.ForEach(m => m.VerificarSeEstahEmAtividade());
                medicos.ForEach(m => m.VerificarDescanso());

                Log.Logger.Information("medicos selecionados com sucesso");

                return Result.Ok(medicos);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todos os medicos";

                Log.Logger.Error(ex, msgErro);

                throw new Exception(msgErro, ex);
            }
        }
    }
}
