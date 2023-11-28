using FluentValidation;

namespace eAgendaMedica.Dominio
{
    public static class RuleBuilderExtensions
    {
        /// <summary>
        /// //https://medium.com/@igorrozani/criando-uma-express%C3%A3o-regular-para-telefone-fef7a8f98828
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilder<T, string> CRM<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .Matches(@"[0-9]{5}-[A-Z]{2}");

            return options;
        }



    }




}
