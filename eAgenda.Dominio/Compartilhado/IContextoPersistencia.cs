using System.Threading.Tasks;

namespace eAgendaMedica.Dominio
{
    public interface IContextoPersistencia
    {
        void DesfazerAlteracoes();

        void GravarDados();

        Task GravarDadosAsync();
    }
}
