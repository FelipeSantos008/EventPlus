using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI;

public interface IEventoRepository
{   
    void Cadastrar(Evento evento);
    List<Evento> Listar();
    void Deletar(Guid id);
    void Atualizar(Guid id, Evento evento);
    List<Evento> ListarPorId(Guid IdUsuario);
    List<Evento> ProximoEvento();
    Evento BuscarPorId(Guid id);
}
