using AgendaApp.API.Dtos.Tarefas;
using AgendaApp.API.Dtos.Usuarios;
using AgendaApp.Domain.Entities;
using AutoMapper;

namespace AgendaApp.API.Mappings
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<CriarUsuarioRequestDto, Usuario>()
                .AfterMap((dto, entity) =>
                {
                    entity.Id = Guid.NewGuid();
                });

            CreateMap<Usuario, CriarUsuarioResponseDto>()
                .AfterMap((entity, dto) =>
                {
                    dto.DataHoraCadastro = DateTime.Now;
                });

            CreateMap<Usuario, AutenticarUsuarioResponseDto>()
                .AfterMap((entity, dto) =>
                {
                    dto.DataHoraAcesso = DateTime.Now;
                });

            CreateMap<CadastroTarefaRequestDto, Tarefa>()
                .AfterMap((dto, entity) =>
                {
                    entity.Id = Guid.NewGuid();
                });

            CreateMap<EdicaoTarefaRequestDto, Tarefa>();

            CreateMap<Tarefa, ConsultaTarefaResponseDto>();
        }
    }
}



