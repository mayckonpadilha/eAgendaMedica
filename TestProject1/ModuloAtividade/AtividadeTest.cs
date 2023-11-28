using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.Test.ModuloAtividade
{
    [TestClass]
    public class AtividadeTest
    {
        [TestMethod]
        public void Atividade_deve_verificar_se_estah_finalizada()
        {
            //arrange
            var a = new Atividade();
            a.DataRealizacao = new DateTime(2023, 11, 21);
            a.HoraInicio = new TimeSpan(01, 2, 2);
            a.HoraTermino = new TimeSpan(1, 1, 1);

            //action
            var resultado = a.VerificarSeAtividadeEstahFinalizada();

            //assert
            Assert.AreEqual(true, resultado);
        }
        [TestMethod]
        public void Atividade_deve_verificar_se_estah_finalizada_por_horario()
        {
            //arrange
            var a = new Atividade();
            a.DataRealizacao = DateTime.Now.Date;
            a.HoraInicio = new TimeSpan(11, 31, 0);
            a.HoraTermino = new TimeSpan(12, 30, 0);

            //action
            var resultado = a.VerificarSeAtividadeEstahFinalizada();

            //assert
            Assert.AreEqual(true, resultado);
        }
    }
}