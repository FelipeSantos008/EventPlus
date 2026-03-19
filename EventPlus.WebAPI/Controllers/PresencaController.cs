using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private IPresencaRepository _presencaRepository;
    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }

    /// <summary>
    /// Endpoint da API que retorna uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param>
    /// <returns>Status cpde 200 e presença buscada</returns>

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_presencaRepository.Listar());
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    
        
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_presencaRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que retorna as presenças de um usuário específico
    /// </summary>
    /// <param name="idUsuario">id do usuário para filtragem</param>
    /// <returns>uma lista de presenças filtradas pelo usuário</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
     public IActionResult BuscarPorUsuario(Guid idUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Endpoint da API para inscrever um usuário em um evento, criando uma nova presença
    /// </summary>
    /// <param name="presenca">Presença a ser cadastrada</param>
    /// <returns>Status code 201</returns>
    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presenca)
    {
        try
        {
            var novaPresenca = new Presenca
            {
                IdPresenca = Guid.NewGuid(),
                Situacao = presenca.Situacao,
                IdUsuario = presenca.IdUsuario,
                IdEvento = presenca.IdEvento
            };
            _presencaRepository.Inscrever(novaPresenca);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API para atualizar a situação de uma presença existente
    /// </summary>
    /// <param name="id">id da Presença</param>
    /// <param name="presencaDTO">presença com os dados atualizados</param>
    /// <returns>Status code 204 e a presença atualizada</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, PresencaDTO presencaDTO)
    {
        try
        {
            var presencaAtualizada = new Presenca
            {
                IdPresenca = id,
                Situacao = presencaDTO.Situacao,
                IdUsuario = presencaDTO.IdUsuario,
                IdEvento = presencaDTO.IdEvento
            };
            _presencaRepository.Atualizar(id, presencaAtualizada);
            return StatusCode(204, presencaAtualizada);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API para deletar uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser deletada</param>
    /// <returns>status code 204</returns>
    [HttpDelete("{id}")]   
     public IActionResult Deletar(Guid id)
    {
        try
        {
            _presencaRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

