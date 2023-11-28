using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public interface IRepositorioMedico : IRepositorio<Medico>
    {
        List<Medico> SelecionarMuitos(List<Guid> idsSelecionadas);
        List<Medico> SelecionarTop10MedicosMaisTrabalhadores(DateTime dataInicio, DateTime dataTermino);
    }
}
