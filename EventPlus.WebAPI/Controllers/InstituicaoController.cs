using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace EventPlus.WebAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class InstituicaoController : ControllerBase
{
    private readonly IInstituicaoRepository _instituicaoRepository;

    public InstituicaoController(IInstituicaoRepository instituicaoRepository)
    {
        _instituicaoRepository = instituicaoRepository;
    }

    /// <summary>
    /// Endpoint API que chama o metodo de listar instituição
    /// </summary>
    /// <returns>Status code 200</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_instituicaoRepository.Listar());
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    /// <summary>
    /// Endpoint API que chama o metodo para listar uma instituição especifica
    /// </summary>
    /// <param name="id">Id da instituição</param>
    /// <returns>Status code 200 e instituição buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_instituicaoRepository.BuscarPorId(id));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o metodo de cadastrar  instituição
    /// </summary>
    /// <param name="instituicao">Intituição a ser cadastrada</param>
    /// <returns>Status code 201 e a instituição cadastrada</returns>
    [HttpPost]
    public IActionResult Cadastrar(InstituicaoDTO instituicao)
    {
        try
        {
            var novaInstituicao = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,
                Cnpj = instituicao.Cnpj!,
                Endereco = instituicao.Endereco!
            };
            _instituicaoRepository.Cadastrar(novaInstituicao);
            return StatusCode(201);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o metodo de atualizar uma instituição
    /// </summary>
    /// <param name="id">Id Instituição</param>
    /// <param name="instituicao">Instituição com dados atualizado</param>
    /// <returns>Status code 204 e a instituição atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
    {
        try
        {
            var instituicaoAtualizada = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,
                Cnpj = instituicao.Cnpj!,
                Endereco = instituicao.Endereco!    
            };
            _instituicaoRepository.Atualizar(id, instituicaoAtualizada);
            return StatusCode(204, instituicaoAtualizada);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o metodo de deletar um tipo de usuario
    /// </summary>
    /// <param name="id">Id da instituição a ser deletado</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _instituicaoRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}