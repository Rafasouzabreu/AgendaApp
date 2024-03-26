namespace AgendaApp.API.Dtos.Tarefas
{
    public class EdicaoTarefaRequestDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataHora { get; set; }
        public string? Descricao { get; set; }
        public int? Prioridade { get; set; }
    }
}
