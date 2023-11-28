using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.WebAPI.ViewModels.ModuloMedico
{
        public class ListarMedicoViewModel
        {
            public ListarMedicoViewModel()
            {
                HorasOcupadas = new List<HorasOcupadasViewModel>();
            }
            public Guid Id { get; set; }
            public string CRM { get; set; }
            public string Nome { get; set; }
            public bool EmAtividade { get; set; }
            public string HorasDeDescanso { get; set; }
            public List<HorasOcupadasViewModel> HorasOcupadas { get; set; }
    }
}
