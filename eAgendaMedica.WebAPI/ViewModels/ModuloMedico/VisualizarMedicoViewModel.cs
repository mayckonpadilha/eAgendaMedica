using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebAPI.ViewModels.ModuloAtividade;

namespace eAgendaMedica.WebAPI.ViewModels.ModuloMedico
{
    public class VisualizarMedicoViewModel
    {
        public VisualizarMedicoViewModel()
        {
            Atividades = new List<ListarAtividadeViewModel>();
            HorasOcupadas = new List<HorasOcupadasViewModel>();
        }
        public Guid Id { get; set; }
        public string CRM { get; set; }
        public string Nome { get; set; }
        public bool EmAtividade { get; set; }

        public string HorasDeDescanso { get; set; }
        public List<ListarAtividadeViewModel> Atividades { get; set; }
        public List<HorasOcupadasViewModel> HorasOcupadas { get; set; }
    }
}
