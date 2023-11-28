using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebAPI.ViewModels.ModuloMedico;

namespace eAgendaMedica.WebAPI.ViewModels.ModuloAtividade
{
    public class FormAtividadeViewModel
    {
        public string Assunto { get; set; }
        public DateTime DataRealizacao { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }
        public TipoAtividadeEnum TipoAtividadeEnum { get; set; }
    }
}
