
using AgendaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Domain.Interfaces.Services
{
    public interface ITarefaDomainService
    {
        void CriarTarefa(Tarefa tarefa);
        void AlterarTarefa(Tarefa tarefa);
        void ExcluirTarefa(Guid id, Guid usuarioId);

        List<Tarefa> Consultar(DateTime dataMin, DateTime dataMax, Guid usuarioId);
        Tarefa? ObterPorId(Guid id, Guid usuarioId);
    }
}
