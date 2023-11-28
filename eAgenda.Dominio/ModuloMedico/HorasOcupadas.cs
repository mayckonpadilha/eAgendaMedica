using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloAtividade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaMedica.Dominio.ModuloMedico
{
    public class HoraOcupada : EntidadeBase<HoraOcupada>
    {
        public DateTime DiaDaAtividade{ get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFinal { get; set; }

        public HoraOcupada()
        {

        }
        public HoraOcupada(DateTime da, TimeSpan hi, TimeSpan hf)
        {
            DiaDaAtividade = da;
            HoraInicio = hi;
            HoraFinal = hf;
        }

        public override bool Equals(object? obj)
        {
            return obj is HoraOcupada horaOcupada &&
                  DiaDaAtividade == horaOcupada.DiaDaAtividade &&
                  HoraInicio == horaOcupada.HoraInicio &&
                  HoraFinal == horaOcupada.HoraFinal;

        }

        public override void Atualizar(HoraOcupada registro)
        {
            Id = registro.Id;
            DiaDaAtividade = registro.DiaDaAtividade;
            HoraInicio = registro.HoraInicio;
            HoraFinal = registro.HoraFinal;
        }
    }
}
