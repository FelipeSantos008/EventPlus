using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _eventContext;
    public PresencaRepository(EventContext eventContext)
    {
        _eventContext = eventContext; 
    }

    public void Atualizar(Guid id, Presenca presenca)
    {
         var presencaBuscada = _eventContext.Presencas.Find(id);
        if (presencaBuscada != null)
            {
            presencaBuscada.Situacao = presenca.Situacao;

            _eventContext.SaveChanges();
        }
    }

    /// <summary>
    /// Busca uma presença por seu ID
    /// </summary>
    /// <param name="id">id da presençaa a ser buscada</param>
    /// <returns>presença buscada</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .First(p => p.IdPresenca == id)!;
    }

    public void Deletar(Guid id)
    {
        var presencaBuscada = _eventContext.Presencas.Find(id);
        if (presencaBuscada != null)
        {
            _eventContext.Presencas.Remove(presencaBuscada);
        }
    }

    public void Inscrever(Presenca Inscricao)
    {
        _eventContext.Presencas.Add(Inscricao);
         _eventContext.SaveChanges();
    }

    public List<Presenca> Listar(Guid id)
    {
        return _eventContext.Presencas.OrderBy(Presenca => Presenca.Situacao).ToList();
    }

    public List<Presenca> Listar()
    {
        return _eventContext.Presencas
            .OrderBy(Presenca => Presenca.Situacao)
            .ToList();
    }

    /// <summary>
    /// Lista as presenças de um usuário específico
    /// </summary>
    /// <param name="idUsuario">id do usuário para filtragem</param>
    /// <returns>lista de presenças de um usuário especificos</returns>
    public List<Presenca> ListarMinhas(Guid idUsuario)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
            .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == idUsuario)
            .ToList();
    }
}
