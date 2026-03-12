using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoEventoController : ControllerBase
{
    private ITipoEventoRepository _tipoEventoRepository;
    public TipoEventoController(ITipoEventoRepository tipoEventoRepository)
    {
        _tipoEventoRepository = tipoEventoRepository;
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o metodo de listar os tipos de evento
    /// </summary>
    /// <returns>Status code 200 e a lista de tipos de eventos</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoEventoRepository.Listar());
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o metodo de buscar um tipo de evento especifico
    /// </summary>
    /// <param name="id">Id do tipo de evento buscado</param>
    /// <returns>Status code 200 e tipo de evento buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id) 
    {
        try
        {
            return Ok(_tipoEventoRepository.BuscarPorId(id));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o metodo de cadastrar um tipo de evento
    /// </summary>
    /// <param name="tipoEvento">Tipo de envento a ser cadastrado</param>
    /// <returns>Stataus code 201 e o tipo de evento cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoEventoDTO tipoEvento)
    {
        try
        {
            var novoTipoEvento = new TipoEvento
            {
                Titulo = tipoEvento.Titulo!
            };
            _tipoEventoRepository.Cadastrar(novoTipoEvento);
            return StatusCode(201);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o metodo de atualizar um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipoEvento</param>
    /// <param name="tipoEvento">Tipo de evento com os dados atualizados</param>
    /// <returns>Status code 204 e o tipo de evneto atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoEventoDTO tipoEvento) 
    {
        try
        {
            var tipoEventoAtualizado = new TipoEvento
            {
                Titulo = tipoEvento.Titulo!
            };
            _tipoEventoRepository.Atualizar(id, tipoEventoAtualizado);
            return StatusCode(204, tipoEventoAtualizado);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o metodo de deletar um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipo do evento a ser excluído</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
     public IActionResult Deletar(Guid id) 
    {
        try
        {
            _tipoEventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}
