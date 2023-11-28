using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaMedica.Dominio.Test.ModuloMedico
{
    [TestClass]
    public class ValidadorMedicoTest
    {
        public ValidadorMedicoTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Crm_do_Medico_deve_nao_deve_ser_nulo()
        {
            //arrange
            var t = new Medico();
            t.CRM = null;

            ValidadorMedico validador = new ValidadorMedico();

            //action
            var resultado = validador.Validate(t);

            //assert
            Assert.AreEqual("'CRM' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Crm_do_Medico_deve_ser_informado()
        {
            //arrange
            var t = new Medico();
            t.CRM = "";

            ValidadorMedico validador = new ValidadorMedico();

            //action
            var resultado = validador.Validate(t);

            //assert
            Assert.AreEqual("'CRM' deve ser informado.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Crm_do_Medico_nao_deve_estar_no_formato_incorreto()
        {
            //arrange
            var t = new Medico();
            t.CRM = "AE@EEWdD";

            ValidadorMedico validador = new ValidadorMedico();

            //action
            var resultado = validador.Validate(t);

            //assert
            Assert.AreEqual("'CRM' não está no formato correto.", resultado.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void Nome_do_Medico_nao_deve_ser_nulo()
        {
            //arrange
            var t = new Medico();
            t.CRM = "12345-SP";
            t.Nome = null;

            ValidadorMedico validador = new ValidadorMedico();

            //action
            var resultado = validador.Validate(t);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_do_Medico_deve_ser_informado()
        {
            //arrange
            var t = new Medico();
            t.CRM = "12345-SP";
            t.Nome = "";

            ValidadorMedico validador = new ValidadorMedico();

            //action
            var resultado = validador.Validate(t);

            //assert
            Assert.AreEqual("'Nome' deve ser informado.", resultado.Errors[0].ErrorMessage);
        }
    }
}
