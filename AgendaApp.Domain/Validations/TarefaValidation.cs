using AgendaApp.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Domain.Validations
{
    public class TarefaValidation : AbstractValidator<Tarefa>
    {
        public TarefaValidation()
        {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("O id da tarefa é obrigatório.");

            RuleFor(t => t.Nome)
                .NotEmpty().WithMessage("O nome da tarefa é obrigatório.")
                .Length(8, 100).WithMessage("O nome da tarefa deve ter de 8 a 100 caracteres.");

            RuleFor(t => t.DataHora)
                .NotEmpty().WithMessage("A data e hora da tarefa é obrigatório.");

            RuleFor(t => t.Descricao)
                .NotEmpty().WithMessage("A descrição da tarefa é obrigatória.")
                .Length(8, 250).WithMessage("A descrição da tarefa deve ter de 8 a 250 caracteres.");

            RuleFor(t => t.Prioridade)
                .NotEmpty().WithMessage("A prioridade da tarefa é obrigatória.");

            RuleFor(t => t.UsuarioId)
                .NotEmpty().WithMessage("O id do usuário da tarefa é obrigatório.");
        }
    }
}



