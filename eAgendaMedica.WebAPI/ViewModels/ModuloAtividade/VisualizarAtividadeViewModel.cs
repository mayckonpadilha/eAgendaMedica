using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebAPI.ViewModels.ModuloAtividade;

namespace eAgendaMedica.WebAPI.ViewModels.ModuloMedico
{
    public class VisualizarAtividadeViewModel
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public DateTime DataRealizacao { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }
        public int TipoAtividadeEnum { get; set; }
        public bool Finalizada { get; set; }
        public List<ListarMedicoViewModel> Medicos { get; set; }
    }
}
