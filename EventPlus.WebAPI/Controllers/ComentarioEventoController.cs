<<<<<<< HEAD
﻿using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
=======
﻿using EventPlus.WebAPI.Interfaces;
>>>>>>> f44c4e2eab0f3fcc6218a1b20b84426e473bf849
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
<<<<<<< HEAD
    private readonly ContentSafetyClient _contentSafetyClient;
    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(ContentSafetyClient contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }

    [HttpGet("{idEvento} {idUsuario}")]
    public IActionResult BuscarPorUsuario(Guid idUsuario, Guid idEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.BuscarPorIdUsuario(idUsuario, idEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("evento/{IdEvento}")]
    public IActionResult BuscarPorEvento(Guid IdEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.Listar(IdEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ListarExibe/{IdEvento}")]
    public IActionResult ListarExibe(Guid IdEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(IdEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    /// <summary>
    /// Endpoint da API que cadastrar e modera um comentario
    /// </summary>
    /// <param name="comentarioEvento">comentario a ser moderado</param>
    /// <returns>Status code 201 e comentario criado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(ComentarioEventoDTO comentarioEvento)
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioEvento.Descricao))
            {
                return BadRequest("O texto a ser moderado não pode estar vazio.");
            }

            //criar objeto de análise 
            var request = new AnalyzeTextOptions(comentarioEvento.Descricao);

            //chama a API da azure content safety
            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);

            //verificar se o texto tem alguma severidade maior que 0
            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(c => c.Severity > 0);

            var novoComentario = new ComentarioEvento
            {
                Descricao = comentarioEvento.Descricao,
                IdUsuario = comentarioEvento.IdUsuario,
                IdEvento = comentarioEvento.IdEvento,
                DataComentario = DateTime.Now,
                //Define se o comentario vai ser exibido
                Exibe = !temConteudoImproprio
            };

            //cadastrar o comentário no banco de dados
            _comentarioEventoRepository.Cadastrar(novoComentario);
            return StatusCode(201, novoComentario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpDelete]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _comentarioEventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}



=======
    private readonly IComentarioEventoRepository _comentarioEventoRepository;
    public ComentarioEventoController(IComentarioEventoRepository comentarioEventoRepository)
    {
        _comentarioEventoRepository = comentarioEventoRepository;
    }


}
>>>>>>> f44c4e2eab0f3fcc6218a1b20b84426e473bf849
