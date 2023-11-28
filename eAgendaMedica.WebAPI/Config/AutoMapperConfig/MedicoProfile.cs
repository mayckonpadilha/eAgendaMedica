using AutoMapper;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebAPI.ViewModels.ModuloMedico;

namespace eAgenda.WebApi.Config.AutoMapperConfig
{
    public class MedicoProfile : Profile
    {
        public MedicoProfile()
        {
            CreateMap<Medico, ListarMedicoViewModel>();

            CreateMap<Medico, VisualizarMedicoViewModel>();
            
            CreateMap<FormMedicoViewModel, Medico>();
        }
    }
}