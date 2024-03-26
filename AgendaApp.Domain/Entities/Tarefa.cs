using AgendaApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Domain.Entities
{
    public class Tarefa
    {
        #region Propriedades

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataHora { get; set; }
        public string? Descricao { get; set; }
        public PrioridadeTarefa? Prioridade { get; set; }
        public Guid? UsuarioId { get; set; }

        #endregion

        #region Relacionamentos

        public Usuario? Usuario { get; set; }

        #endregion
    }
}



