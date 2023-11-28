using eAgendaMedica.Dominio;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaMedica.Infra.Orm.ModuloAtividade
{
    public class RepositorioAtividadeOrm : RepositorioBase<Atividade>, IRepositorioAtividade
    {
        public RepositorioAtividadeOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }
        public override Atividade SelecionarPorId(Guid id)
        {
            return registros
                .Include(x => x.Medicos)
                .SingleOrDefault(x => x.Id == id);
        }

    }
}
