using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.Test.ModuloMedico
{
    [TestClass]
    public class AtividadeTest
    {
        [TestMethod]
        public void Medico_Nao_Deve_Ter_Dois_Horarios_Iguais()
        {
            //arrange
            var m = new Medico();
            HoraOcupada hora = new HoraOcupada(DateTime.UtcNow, new TimeSpan(3000), new TimeSpan(4000));
            m.AdicionarHorario(hora);

            //action
            var resultado = m.VerificarHorarioLivre(hora);

            //assert
            Assert.AreEqual(false, resultado);
        }

        [TestMethod]
        public void Medico_Deve_inserir_uma_atividade()
        {
            //arrange
            var m = new Medico();
            Atividade atividade = new Atividade();
            atividade.DataRealizacao = DateTime.UtcNow;
            atividade.HoraInicio = new TimeSpan(1000);
            atividade.HoraTermino = new TimeSpan(2000);
            m.AdicionarAtividade(atividade);

            //action
            var resultado = m.Atividades.Count();

            //assert
            Assert.AreEqual(1, resultado);
        }

        [TestMethod]
        public void Medico_calcular_suas_horas_de_trabalho()
        {
            //arrange
            var m = new Medico();
            m.AdicionarHorario(DateTime.UtcNow.AddMinutes(10), new TimeSpan(1000), new TimeSpan(2000));
            m.AdicionarHorario(DateTime.UtcNow.AddDays(3), new TimeSpan(1000), new TimeSpan(2000));

            //action
            var resultado = m.CalcularHorasOcupadas(DateTime.UtcNow, DateTime.UtcNow.AddDays(1));

            //assert
            Assert.AreEqual(new TimeSpan(1000), resultado);

        }


        [TestMethod]
        public void Medico_deve_verificar_se_estah_em_atividade()
        {
            //arrange
            var m = new Medico();
            m.AdicionarHorario(DateTime.UtcNow.AddMinutes(10), new TimeSpan(1000), new TimeSpan(2000));
            m.AdicionarHorario(DateTime.UtcNow,DateTime.UtcNow.TimeOfDay.Add(new TimeSpan(0, 1, 0)), DateTime.UtcNow.TimeOfDay.Add(new TimeSpan(1, 1, 1)));

            //action
            var resultado = m.VerificarSeEstahEmAtividade();

            //assert
            Assert.AreEqual(true, resultado) ;

        }

        [TestMethod]
        public void Medico_deve_verificar_o_tempo_de_descanso()
        {
            //arrange
            var m = new Medico();
            var a = new Atividade();
            a.DataRealizacao = DateTime.Now.Date;
            a.HoraTermino = new TimeSpan(15, 49, 00);
            a.TipoAtividadeEnum = TipoAtividadeEnum.Consulta;
            m.Atividades.Add(a);
            a.AtribuirAtividade();

            //action
            var resultado = m.VerificarDescanso();

            //assert
            Assert.AreEqual(true, resultado);

        }

        [TestMethod]
        public void Medico_deve_verificar_o_tempo_de_descanso_em_cirurgias()
        {
            //arrange
            var m = new Medico();
            var a = new Atividade();
            a.DataRealizacao = DateTime.Now.Date;
            a.HoraTermino = new TimeSpan(15, 49, 00);
            a.TipoAtividadeEnum = TipoAtividadeEnum.Cirurgia;
            m.Atividades.Add(a);
            a.AtribuirAtividade();

            //action
            var resultado = m.VerificarDescanso();

            //assert
            Assert.AreEqual(true, resultado);

        }

    }
}