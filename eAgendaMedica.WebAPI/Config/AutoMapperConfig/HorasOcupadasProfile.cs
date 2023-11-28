using AutoMapper;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebAPI.ViewModels.ModuloMedico;

namespace eAgendaMedica.WebAPI.Config.AutoMapperConfig
{
    public class HorasOcupadasProfile : Profile
    {
        public HorasOcupadasProfile()
        {

            CreateMap<HoraOcupada, HorasOcupadasViewModel>();
        }
    }
}
