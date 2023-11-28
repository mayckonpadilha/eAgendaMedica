using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio;
using FluentResults;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgendaMedica.Aplicacao.ModuloMedico;

namespace eAgendaMedica.Aplicacao.ModuloAtividade
{
    public class ServicoAtividade : ServicoBase<Atividade, ValidadorAtividade>
    {
        private IRepositorioAtividade repositorioAtividade;
        private IContextoPersistencia contextoPersistencia;
        private ServicoMedico servicoMedico;
        public ServicoAtividade(IRepositorioAtividade repositorioAtividade,
                             IContextoPersistencia contextoPersistencia,ServicoMedico servicoMedico)
        {
            this.repositorioAtividade = repositorioAtividade;
            this.contextoPersistencia = contextoPersistencia;
            this.servicoMedico = servicoMedico;
        }

        public async Task<Result<Atividade>> Inserir(Atividade Atividade)
        {
            Log.Logger.Debug("Tentando inserir Atividade... {@c}", Atividade);

            Result resultado = Validar(Atividade);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioAtividade.Inserir(Atividade);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Atividade {AtividadeId} inserido com sucesso", Atividade.Id);

                return Result.Ok(Atividade);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar inserir o Atividade";

                Log.Logger.Error(ex, msgErro + " {AtividadeId} ", Atividade.Id);

                throw new Exception(msgErro, ex);
            }
        }

        public async Task<Result<Atividade>> Editar(Atividade Atividade)
        {
            Log.Logger.Debug("Tentando editar Atividade... {@c}", Atividade);

            var resultado = Validar(Atividade);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioAtividade.Editar(Atividade);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Atividade {AtividadeId} editado com sucesso", Atividade.Id);
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar editar o Atividade";

                Log.Logger.Error(ex, msgErro + " {AtividadeId}", Atividade.Id);

                throw new Exception(msgErro, ex);
            }

            return Result.Ok(Atividade);
        }

        public async Task<Result> Excluir(Guid id)
        {
            var AtividadeResult = await SelecionarPorId(id);

            if (AtividadeResult.IsSuccess)
                return Excluir(AtividadeResult.Value);

            return Result.Fail(AtividadeResult.Errors);
        }

        public Result Excluir(Atividade Atividade)
        {
            Log.Logger.Debug("Tentando excluir Atividade... {@c}", Atividade);

            try
            {
                repositorioAtividade.Excluir(Atividade);

                contextoPersistencia.GravarDados();

                Log.Logger.Information("Atividade {AtividadeId} editado com sucesso", Atividade.Id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar excluir o Atividade";

                Log.Logger.Error(ex, msgErro + " {AtividadeId}", Atividade.Id);

                throw new Exception(msgErro, ex);
            }
        }

        public async Task<Result<List<Atividade>>> SelecionarTodos()
        {
            Log.Logger.Debug("Tentando selecionar Atividades...");

            try
            {
                var Atividades = repositorioAtividade.SelecionarTodos();

                Atividades.ForEach(a => a.VerificarSeAtividadeEstahFinalizada());

                Log.Logger.Information("Atividades selecionados com sucesso");

                return Result.Ok(Atividades);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todos os Atividades";

                Log.Logger.Error(ex, msgErro);

                throw new Exception(msgErro, ex);
            }
        }

        public async Task<Result<Atividade>> SelecionarPorId(Guid id)
        {
            Log.Logger.Debug("Tentando selecionar Atividade {AtividadeId}...", id);

            try
            {
                var Atividade = repositorioAtividade.SelecionarPorId(id);

                Atividade.VerificarSeAtividadeEstahFinalizada();

                if (Atividade == null)
                {
                    Log.Logger.Warning("Atividade {AtividadeId} não encontrado", id);

                    return Result.Fail("Atividade não encontrado");
                }

                Log.Logger.Information("Atividade {AtividadeId} selecionado com sucesso", id);

                return Result.Ok(Atividade);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar o Atividade";

                Log.Logger.Error(ex, msgErro + " {AtividadeId}", id);

                throw new Exception(msgErro, ex);
            }
        }

    }
}
