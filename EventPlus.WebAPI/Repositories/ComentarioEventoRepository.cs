using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class ComentarioEventoRepository : IComentarioEventoRepository
{
    private readonly EventContext _eventContext;
    public ComentarioEventoRepository(EventContext eventContext)
    {
        _eventContext = eventContext;
    }

    public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        return _eventContext.ComentarioEventos
            .Include(c => c.IdUsuarioNavigation)
           .Include(p => p.IdEventoNavigation)
           .ThenInclude(e => e!.IdInstituicaoNavigation)
           .First(p => p.IdUsuario == IdEvento)!;
    }

    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _eventContext.ComentarioEventos.Add(comentarioEvento);
        _eventContext.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        _eventContext.ComentarioEventos.Remove(_eventContext.ComentarioEventos.Find(id)!);
         _eventContext.SaveChanges();
    }

    public List<ComentarioEvento> Listar(Guid IdEvento)
    {
        return _eventContext.ComentarioEventos
            .OrderBy(ComentarioEvento => ComentarioEvento.Descricao)
            .ToList();

    }

    public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
    {
    
        return _eventContext.ComentarioEventos
            .OrderBy(ComentarioEvento => ComentarioEvento.Exibe)
            .ToList();
}
}
