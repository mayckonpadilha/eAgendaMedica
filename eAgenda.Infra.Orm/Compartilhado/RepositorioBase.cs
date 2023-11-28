using eAgendaMedica.Dominio;
using eAgendaMedica.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eAgendaMedica.Infra.Orm.Compartilhado
{
    public abstract class RepositorioBase<TEntity> where TEntity : EntidadeBase<TEntity>
    {
        protected DbSet<TEntity> registros;
        private readonly eAgendaMedicaDbContext dbContext;

        public RepositorioBase(IContextoPersistencia contextoPersistencia)
        {
            dbContext = (eAgendaMedicaDbContext)contextoPersistencia;
            registros = dbContext.Set<TEntity>();
        }

        public virtual void Inserir(TEntity novoRegistro)
        {
            registros.Add(novoRegistro);
        }
        public virtual void InserirAsync(TEntity novoRegistro)
        {
            registros.AddAsync(novoRegistro);
        }

        public virtual void Editar(TEntity registro)
        {
            registros.Update(registro);
        }

        public virtual void Excluir(TEntity registro)
        {
            registros.Remove(registro);
        }

        public virtual TEntity SelecionarPorId(Guid id)
        {
            return registros
                .SingleOrDefault(x => x.Id == id);
        }

        public virtual Task<TEntity> SelecionarPorIdAsync(Guid id)
        {
            return registros
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual List<TEntity> SelecionarTodos()
        {
            return registros.ToList();
        }

        public virtual async Task<List<TEntity>> SelecionarTodosAsync()
        {
            return await registros.ToListAsync();
        }
    }
}
