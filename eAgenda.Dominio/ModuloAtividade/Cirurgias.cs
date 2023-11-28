using eAgendaMedica.Dominio.ModuloMedico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class Cirurgias : TipoAtividade
    {
     
        public Cirurgias()
        {
            TempoDeDescanso = new TimeSpan(4,0,0);
        }
    }
}
