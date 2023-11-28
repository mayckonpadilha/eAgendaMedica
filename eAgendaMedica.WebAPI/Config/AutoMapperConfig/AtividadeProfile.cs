using AutoMapper;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebAPI.ViewModels.ModuloAtividade;
using eAgendaMedica.WebAPI.ViewModels.ModuloMedico;

namespace eAgendaMedica.WebAPI.Config.AutoMapperConfig
{
    public class AtividadeProfile : Profile
    {
        public AtividadeProfile() 
        {
            CreateMap<Atividade, ListarAtividadeViewModel>();

            CreateMap<Atividade, VisualizarAtividadeViewModel>();

            CreateMap<FormAtividadeViewModel, Atividade>();

            CreateMap<InserirAtividadeViewModel, Atividade>()
                .ForMember(destino => destino.Medicos, opt => opt.Ignore())
                .ForMember(destino => destino.TipoAtividadeEnum, opt => opt.Ignore())
                .AfterMap<InserirAtividadeMappingAction>()
                .AfterMap<InserirTipoDeAtividadeMappingAction>();

        }
    }

    public class InserirAtividadeMappingAction : IMappingAction<InserirAtividadeViewModel, Atividade>
    {
        private readonly IRepositorioMedico repositorioMedico;
        public InserirAtividadeMappingAction(IRepositorioMedico repositorioMedico)
        {
            this.repositorioMedico = repositorioMedico;
        }
        public void Process(InserirAtividadeViewModel source, Atividade destination, ResolutionContext context)
        {
            destination.Medicos = new List<Medico>();

            var Medicos = repositorioMedico.SelecionarMuitos(source.IdsMedicos);

            foreach (var m in Medicos)
            {
                try
                {

                    destination.AdicionarMedico(m);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }

    public class InserirTipoDeAtividadeMappingAction : IMappingAction<InserirAtividadeViewModel, Atividade>
    {
        public InserirTipoDeAtividadeMappingAction()
        {
        }
        public void Process(InserirAtividadeViewModel source, Atividade destination, ResolutionContext context)
        {
           if(source.TipoAtividadeEnum == TipoAtividadeEnum.Cirurgia)
            {
                destination.TipoAtividadeEnum = 0;
            }
            else
            {
                destination.TipoAtividadeEnum = (TipoAtividadeEnum)1;
            }

            destination.AtribuirAtividade();
        }
    }
}
