
using eAgendaMedica.Dominio.ModuloMedico;
using FluentValidation;

namespace eAgendaMedica.Dominio.ModuloAtividade
{

    public class ValidadorMedico : AbstractValidator<Medico>
    {
        public ValidadorMedico()
        {
            RuleFor(x => x.CRM)
                   .NotNull().NotEmpty().CRM();

            RuleFor(x => x.Nome).NotNull().NotEmpty();

        }
    }
}
