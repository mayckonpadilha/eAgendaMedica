using eAgendaMedica.Dominio.ModuloAtividade;
using FluentValidation;
using FluentValidation.Validators;

namespace eAgendaMedica.Dominio.ModuloAtividade
{

    public class ValidadorAtividade : AbstractValidator<Atividade>
    {
        public ValidadorAtividade()
        {

            RuleFor(x => x.DataRealizacao)
                   .NotNull().NotEmpty();

            RuleFor(x => x.Medicos).NotEmpty();


            RuleFor(x => x.HoraInicio).LessThan(x => x.HoraTermino)
                    .WithMessage("Horário de ínicio deve ser menor que Horário de Términio");
        }
        
    }
}

