using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;
    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    /// <summary>
    /// EndPoint da api que faz chamada para o metodo de listar eventos pelo id do usuario
    /// </summary>
    /// <param name="IdUsuario">id do usuario praa filtragem</param>
    /// <returns>Status code 200 e uma lista de eventos =</returns>
    [HttpGet("Usuario/{IdUsuario}")]
    public IActionResult ListarPorId(Guid IdUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(IdUsuario));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da api que faz chamada para o metodo de listar os proximos eventos
    /// </summary>
    /// <returns>Status code 200 e a lista de proximos eventos</returns>
    [HttpGet("ListarProximo")]
    public IActionResult BuscarProximosEventos()
    {
        try
        {
            return Ok(_eventoRepository.ProximoEvento());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Cadastrar(Evento evento)
    {
        var novoEvento = new Evento
        {
            //IdEvento = Guid.NewGuid(),
            Nome = evento.Nome,
            DataEvento = evento.DataEvento,
            Descricao = evento.Descricao
        };
        try
        {
            _eventoRepository.Cadastrar(evento);
            return StatusCode(201);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Atualiazar(Guid id, EventoDTO evento)
    {
        try
        {
            var eventoAtualizado = new Evento
            {
                Nome = evento.Nome!,
                DataEvento = evento.DataEvento,
                Descricao = evento.Descricao!
            };
            _eventoRepository.Atualizar(id, eventoAtualizado);
            return StatusCode(204, eventoAtualizado);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
     [HttpDelete("{id}")]
     public IActionResult Deletar(Guid id)
     {
         try
         {
             _eventoRepository.Deletar(id);
             return StatusCode(204);
         }
         catch (Exception error)
         {
             return BadRequest(error.Message);
         }
    }
}
