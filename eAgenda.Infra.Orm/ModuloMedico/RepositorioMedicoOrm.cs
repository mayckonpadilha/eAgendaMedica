
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio;
using eAgendaMedica.Infra.Orm.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eAgendaMedica.Dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Infra.Orm.ModuloMedico
{
    public class RepositorioMedicoOrm : RepositorioBase<Medico>, IRepositorioMedico
    {
        public RepositorioMedicoOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }

        public override List<Medico> SelecionarTodos()
        {
            return registros.Include(x => x.Atividades).Include(x => x.HorasOcupadas).ToList();
        }
        public override Medico SelecionarPorId(Guid id)
        {
            return registros
                .Include(x => x.Atividades)
                .Include(x => x.HorasOcupadas)
                .SingleOrDefault(x => x.Id == id);
        }
        public List<Medico> SelecionarMuitos(List<Guid> idsSelecionadas)
        {
            return registros.Where(medico => idsSelecionadas.Contains(medico.Id))
                .Include(x => x.Atividades)
                .Include(x => x.HorasOcupadas).ToList();
        }

        public List<Medico> SelecionarTop10MedicosMaisTrabalhadores(DateTime dataInicio, DateTime dataTermino)
        {
            return registros
                .Include(x => x.Atividades)
                .Include(x => x.HorasOcupadas)
                .AsEnumerable()
                .OrderByDescending(x => x.CalcularHorasOcupadas(dataInicio, dataTermino)).Take(10).ToList();
        }
    }
}
