using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.ModuloMedico;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Infra.Orm.Test.ModuloMedico
{
    [TestClass]
    public class RepositorioMedicoOrmTest
    {
        private readonly eAgendaMedicaDbContext db;

        public RepositorioMedicoOrmTest()
        {
            var builder = new DbContextOptionsBuilder<eAgendaMedicaDbContext>();

            builder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=eAgendaMedicaOrm;Integrated Security=True");

            db = new eAgendaMedicaDbContext(builder.Options);

            db.Set<Medico>().RemoveRange(db.Set<Medico>());

            db.SaveChanges();
        }

        [TestMethod]
        public void Deve_inserir_medico()
        {
            Medico novoMedico = new Medico() { Nome = "Medico_1",CRM = "12345-SP",EmAtividade = false};

            var repositorio = new RepositorioMedicoOrm(db);

            //action
            repositorio.Inserir(novoMedico);
            db.SaveChanges();

            //assert
            Medico MedicoEncontrado = repositorio.SelecionarPorId(novoMedico.Id);

            Assert.IsNotNull(MedicoEncontrado);
            Assert.AreEqual(novoMedico.Id, MedicoEncontrado.Id);
            Assert.AreEqual(novoMedico.Nome, MedicoEncontrado.Nome);
            Assert.AreEqual(novoMedico.EmAtividade, MedicoEncontrado.EmAtividade);

        }

        [TestMethod]
        public void Deve_editar_medico()
        {
            //arrange
            Medico novoMedico = new Medico() { Nome = "Medico_1", CRM = "12345-SP", EmAtividade = false };
            var repositorio = new RepositorioMedicoOrm(db);
            repositorio.Inserir(novoMedico);
            db.SaveChanges();

            Medico medicoAtualizada = repositorio.SelecionarPorId(novoMedico.Id);
            medicoAtualizada.Nome = "Medico ALterado";
            medicoAtualizada.CRM = "99999-TP";
            medicoAtualizada.EmAtividade = true;

            //action
            repositorio.Editar(medicoAtualizada);
            db.SaveChanges();

            //assert
            Medico medicoEncontrado = repositorio.SelecionarPorId(novoMedico.Id);

            Assert.IsNotNull(medicoEncontrado);
            Assert.AreEqual(medicoAtualizada.Id, medicoEncontrado.Id);
            Assert.AreEqual(medicoAtualizada.Nome, medicoEncontrado.Nome);
            Assert.AreEqual(medicoAtualizada.EmAtividade, medicoEncontrado.EmAtividade);
        }

        [TestMethod]
        public void Deve_excluir_medico()
        {
            //arrange
            Medico novoMedico = new Medico() { Nome = "Medico_1", CRM = "12345-SP", EmAtividade = false };
            var repositorio = new RepositorioMedicoOrm(db);
            repositorio.Inserir(novoMedico);
            db.SaveChanges();

            //action
            repositorio.Excluir(novoMedico);
            db.SaveChanges();

            //assert
            Medico tarefaEncontrada = repositorio.SelecionarPorId(novoMedico.Id);

            Assert.IsNull(tarefaEncontrada);
        }


        [TestMethod]
        public void Deve_selecionar_todos_os_medicos()
        {
            //arrange
            var repositorio = new RepositorioMedicoOrm(db);

            Medico m1 = new Medico() { Nome = "Medico_1", CRM = "12345-SP", EmAtividade = false };
            repositorio.Inserir(m1);

            Medico m2 = new Medico() { Nome = "Medico_2", CRM = "12345-SP", EmAtividade = false };
            repositorio.Inserir(m2);

            Medico m3 = new Medico() { Nome = "Medico_3", CRM = "12345-SP", EmAtividade = false };
            repositorio.Inserir(m3);
            db.SaveChanges();

            //action
            var medicos = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, medicos.Count);

            Assert.AreEqual("Medico_1", medicos[0].Nome);
            Assert.AreEqual("Medico_2", medicos[1].Nome);
            Assert.AreEqual("Medico_3", medicos[2].Nome);
        }

        [TestMethod]
        public void Deve_selecionar_o_top_10_medicos_que_mais_trabalharam()
        {
            //arrange
            var repositorio = new RepositorioMedicoOrm(db);

            AdicionarMedicosComHorario(repositorio);

            db.SaveChanges();

            //action
            var medicos = repositorio.SelecionarTop10MedicosMaisTrabalhadores(DateTime.UtcNow, DateTime.UtcNow.AddDays(1));

            //assert
            Assert.AreEqual(10, medicos.Count);

            Assert.AreEqual("Medico_3", medicos[0].Nome);
            Assert.AreEqual("Medico_2", medicos[1].Nome);
            Assert.AreEqual("Medico_1", medicos[2].Nome);
        }

        private void AdicionarMedicosComHorario(RepositorioMedicoOrm repositorio)
        {
            Medico m1 = new Medico() { Nome = "Medico_1", CRM = "12345-SP", EmAtividade = false };
            m1.AdicionarHorario(new HoraOcupada(DateTime.UtcNow.AddMinutes(10), new TimeSpan(2000), new TimeSpan(4000)));
            repositorio.Inserir(m1);

            Medico m2 = new Medico() { Nome = "Medico_2", CRM = "12345-SP", EmAtividade = false };
            m2.AdicionarHorario(new HoraOcupada(DateTime.UtcNow.AddMinutes(10), new TimeSpan(2000), new TimeSpan(4000)));
            m2.AdicionarHorario(new HoraOcupada(DateTime.UtcNow.AddMinutes(10), new TimeSpan(2000), new TimeSpan(4000)));
            repositorio.Inserir(m2);

            Medico m3 = new Medico() { Nome = "Medico_3", CRM = "12345-SP", EmAtividade = false };
            m3.AdicionarHorario(new HoraOcupada(DateTime.UtcNow.AddMinutes(2), new TimeSpan(2000), new TimeSpan(4000)));
            m3.AdicionarHorario(new HoraOcupada(DateTime.UtcNow.AddMinutes(2), new TimeSpan(2000), new TimeSpan(4000)));
            m3.AdicionarHorario(new HoraOcupada(DateTime.UtcNow.AddMinutes(2), new TimeSpan(2000), new TimeSpan(4000)));
            repositorio.Inserir(m3);

            for (int i = 4; i < 12; i++)
            {
                Medico mi = new Medico() { Nome = $"Medico{i}", CRM = "12345-SP", EmAtividade = false };
                mi.AdicionarHorario(new HoraOcupada(DateTime.UtcNow.AddMinutes(i), new TimeSpan(i+ 000), new TimeSpan(i+ 000)));
                repositorio.Inserir(mi);
            }

        }
    }
}