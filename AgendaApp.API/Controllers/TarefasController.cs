using AgendaApp.API.Dtos.Tarefas;
using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Interfaces.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaDomainService _tarefaDomainService;
        private readonly IMapper _mapper;

        public TarefasController(ITarefaDomainService tarefaDomainService, IMapper mapper)
        {
            _tarefaDomainService = tarefaDomainService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ConsultaTarefaResponseDto), 201)]
        public IActionResult Post(CadastroTarefaRequestDto dto)
        {
            try
            {
                var tarefa = _mapper.Map<Tarefa>(dto);
                tarefa.UsuarioId = Guid.Parse(User.Identity.Name);

                _tarefaDomainService.CriarTarefa(tarefa);

                var response = _mapper.Map<ConsultaTarefaResponseDto>(tarefa);
                return StatusCode(201, response);
            }
            catch (ValidationException e)
            {
                return StatusCode(400, new { e.Errors });
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ConsultaTarefaResponseDto), 200)]
        public IActionResult Put(EdicaoTarefaRequestDto dto)
        {
            try
            {
                var tarefa = _mapper.Map<Tarefa>(dto);
                tarefa.UsuarioId = Guid.Parse(User.Identity.Name);

                _tarefaDomainService.AlterarTarefa(tarefa);

                var response = _mapper.Map<ConsultaTarefaResponseDto>(tarefa);
                return StatusCode(200, response);
            }
            catch (ValidationException e)
            {
                return StatusCode(400, new { e.Errors });
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _tarefaDomainService.ExcluirTarefa(id, Guid.Parse(User.Identity.Name));
                return StatusCode(200, new { id });
            }
            catch (ValidationException e)
            {
                return StatusCode(400, new { e.Errors });
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("{dataMin}/{dataMax}")]
        [ProducesResponseType(typeof(List<ConsultaTarefaResponseDto>), 200)]
        public IActionResult GetAll(DateTime dataMin, DateTime dataMax)
        {
            try
            {
                var lista = _tarefaDomainService.Consultar(dataMin, dataMax, Guid.Parse(User.Identity.Name));
                var response = _mapper.Map<List<ConsultaTarefaResponseDto>>(lista);

                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ConsultaTarefaResponseDto), 200)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var tarefa = _tarefaDomainService.ObterPorId(id, Guid.Parse(User.Identity.Name));
                var response = _mapper.Map<ConsultaTarefaResponseDto>(tarefa);

                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
    }
}



