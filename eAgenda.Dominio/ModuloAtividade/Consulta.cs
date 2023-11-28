using eAgendaMedica.Dominio.ModuloMedico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class Consulta : TipoAtividade
    {
        public Consulta()
        {
            TempoDeDescanso = new TimeSpan(0, 20, 0);
        }
    }
}
