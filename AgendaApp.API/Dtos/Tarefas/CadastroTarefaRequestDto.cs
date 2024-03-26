using AgendaApp.Domain.Enums;

namespace AgendaApp.API.Dtos.Tarefas
{
    public class CadastroTarefaRequestDto
    {
        public string? Nome { get; set; }
        public DateTime? DataHora { get; set; }
        public string? Descricao { get; set; }
        public int? Prioridade { get; set; }
    }
}



