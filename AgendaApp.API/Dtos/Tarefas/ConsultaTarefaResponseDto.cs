
namespace AgendaApp.API.Dtos.Tarefas
{
    public class ConsultaTarefaResponseDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataHora { get; set; }
        public string? Descricao { get; set; }
        public int? Prioridade { get; set; }
        public Guid? UsuarioId { get; set; }
    }
}





