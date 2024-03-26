namespace AgendaApp.API.Dtos.Usuarios
{
    public class CriarUsuarioRequestDto
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
    }
}
