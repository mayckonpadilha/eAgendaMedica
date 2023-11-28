using eAgendaMedica.Dominio.ModuloAtividade;

namespace eAgendaMedica.WebAPI.ViewModels.ModuloAtividade
{
    public class InserirAtividadeViewModel
    {
        public string Assunto { get; set; }
        public DateTime DataRealizacao { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }
        public TipoAtividadeEnum TipoAtividadeEnum { get; set; }
        public List<Guid> IdsMedicos { get; set; }
    }
}
