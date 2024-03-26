using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Interfaces.Repositories;
using AgendaApp.Domain.Interfaces.Services;
using AgendaApp.Domain.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Domain.Services
{
    public class TarefaDomainService : ITarefaDomainService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaDomainService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public void CriarTarefa(Tarefa tarefa)
        {
            #region Validar os dados da tarefa

            var validation = new TarefaValidation().Validate(tarefa);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            #endregion

            _tarefaRepository.Add(tarefa);
        }

        public void AlterarTarefa(Tarefa tarefa)
        {
            #region Validar os dados da tarefa

            var validation = new TarefaValidation().Validate(tarefa);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            #endregion

            #region Verificar se a tarefa não existe no banco de dados

            if (_tarefaRepository.GetById(tarefa.Id) == null)
                throw new ApplicationException("Tarefa não encontrada para edição, verifique o ID informado.");

            #endregion

            _tarefaRepository.Update(tarefa);
        }

        public void ExcluirTarefa(Guid id, Guid usuarioId)
        {
            #region Consultar a tarefa no banco de dados através do id

            var tarefa = _tarefaRepository.GetById(id);
            if (tarefa == null)
                throw new ApplicationException("Tarefa não encontrada para exclusão, verifique o ID informado.");

            #endregion

            #region Verificar se a tarefa não pertence ao usuário autenticado

            if (tarefa.UsuarioId != usuarioId)
                throw new ApplicationException("Tarefa inválida para exclusão.");

            #endregion

            _tarefaRepository.Delete(tarefa);
        }

        public List<Tarefa> Consultar(DateTime dataMin, DateTime dataMax, Guid usuarioId)
        {
            return _tarefaRepository.GetAll(dataMin, dataMax, usuarioId);
        }

        public Tarefa? ObterPorId(Guid id, Guid usuarioId)
        {
            var tarefa = _tarefaRepository.GetById(id);
            if (tarefa != null && tarefa.UsuarioId != usuarioId)
                throw new ApplicationException("Tarefa inválida para consulta.");

            return tarefa;
        }
    }
}



