using AgendaApp.API.Dtos.Usuarios;
using AgendaApp.API.Security;
using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Interfaces.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioDomainService usuarioDomainService, IMapper mapper)
        {
            _usuarioDomainService = usuarioDomainService;
            _mapper = mapper;
        }

        [Route("criar")]
        [HttpPost]
        [ProducesResponseType(typeof(CriarUsuarioResponseDto), 201)]
        public IActionResult Criar(CriarUsuarioRequestDto dto)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(dto);
                _usuarioDomainService.CriarUsuario(usuario);

                var response = _mapper.Map<CriarUsuarioResponseDto>(usuario);
                //HTTP 201 - CREATED
                return StatusCode(201, response);
            }
            catch (ValidationException e)
            {
                //HTTP 400 -> BAD REQUEST
                return StatusCode(400, new { e.Errors });
            }
            catch (ApplicationException e)
            {
                //HTTP 422 - UNPROCESSABLE ENTITY
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { e.Message });
            }
        }

        [Route("autenticar")]
        [HttpPost]
        [ProducesResponseType(typeof(AutenticarUsuarioResponseDto), 200)]
        public IActionResult Autenticar(AutenticarUsuarioRequestDto dto)
        {
            try
            {
                var usuario = _usuarioDomainService.AutenticarUsuario(dto.Email, dto.Senha);
                var response = _mapper.Map<AutenticarUsuarioResponseDto>(usuario);

                response.AccessToken = JwtBearerSecurity.CreateToken(usuario.Id.Value);
                response.DataHoraExpiracao = DateTime.Now.AddHours(JwtBearerSecurity.ExpirationInHours);

                //HTTP 200 -> OK
                return StatusCode(200, response);
            }
            catch (ValidationException e)
            {
                //HTTP 400 -> BAD REQUEST
                return StatusCode(400, new { e.Errors });
            }
            catch (ApplicationException e)
            {
                //HTTP 401 - UNAUTHORIZED
                return StatusCode(401, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { e.Message });
            }
        }
    }
}



