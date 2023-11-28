using eAgendaMedica.Dominio.ModuloMedico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public abstract class TipoAtividade
    {
        public TimeSpan TempoDeDescanso { get; set; }
    }
}
