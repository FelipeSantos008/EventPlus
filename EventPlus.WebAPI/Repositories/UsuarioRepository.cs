using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventContext _context;

    //metodo construtor para injetar o contexto do banco de dados
    public UsuarioRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Busca o usuario por email e valida o hash da senha
    /// </summary>
    /// <param name="Email">Email do usuario a ser buscadp</param>
    /// <param name="Senha">Senha para validar o usuario</param>
    /// <returns>Usuario buscado</returns>
    public Usuario BuscarPorEmail(string Email, string Senha)
    {
        var usuarioBuscado = _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation)
                                              .FirstOrDefault(usuario => usuario.Email == Email);

        //Verifica se o usuario foi encontrado 
        if (usuarioBuscado != null)
        {
            //comparamos o has da senha difirada com o que esta no banco de dados
            bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);
            if (confere)
            {
                return usuarioBuscado;
            }
        }
        return null!;
    }

    /// <summary>
    /// Busca um usuario pelo id incluindo os dados do seu Tipo de Usuario
    /// </summary>
    /// <param name="id">id do usuario a ser buscado</param>
    /// <returns>Usuario buscado e seu tipo de usuario  </returns>
    public Usuario BuscarPorId(Guid id)
    {
        return _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation)
                                .FirstOrDefault(usuario => usuario.IdUsuario == id)!;
    }

    /// <summary>
    /// Cadastra um novo usuário no banco de dados. A senha é criptografada e o Id gerado pelo banco.
    /// </summary>
    /// <param name="usuario">Usuário a ser cadastrado</param>
    public void Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha);

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }
}