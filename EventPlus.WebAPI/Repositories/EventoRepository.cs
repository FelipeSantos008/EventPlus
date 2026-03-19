using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;
    public EventoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Metodo de atualizar um Evento usando o rastreamento automatico
    /// </summary>
    /// <param name="id">id de evento a ser atualizado</param>
    /// <param nam  e="evento">Novos dados do evento</param>
    public void Atualizar(Guid id, Evento evento)
    {
        var eventoBuscado = _context.Eventos.Find(id);
        if (eventoBuscado != null)
        {
            eventoBuscado.Nome = evento.Nome;
            eventoBuscado.Descricao = evento.Descricao;
            eventoBuscado.DataEvento = evento.DataEvento;
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca um evento por id
    /// </summary>
    /// <param name="id">id do evento a ser buscado</param>
    /// <returns>Objeto do Evento com as informações do tipo de evento buscado</returns>
    public Evento BuscarPorId(Guid id)
    {
        return _context.Eventos.Find(id)!;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="evento"></param>
    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var eventoBuscado = _context.Eventos.Find(id);
        if (eventoBuscado != null)
        {
            _context.Eventos.Remove(eventoBuscado);
            _context.SaveChanges();
        }
    }

    public List<Evento> Listar()
    {
        return _context.Eventos.OrderBy(Evento => Evento.Nome).ToList();
    }

    /// <summary>
    /// metodo que busca eventos no qual um usuario confirmou presença
    /// </summary>
    /// <param name="IdUsuario">id do usuario a ser buscado</param>
    /// <returns>uma lista de eventos   </returns>
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true)).ToList();
    }

    /// <summary>
    /// metodo que traz a lista de proxios eventos
    /// </summary>
    /// <returns>uma lista de eventos</returns>
    public List<Evento> ProximoEvento()
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now)
            .OrderBy(e => e.DataEvento)
            .ToList();
    }
}
