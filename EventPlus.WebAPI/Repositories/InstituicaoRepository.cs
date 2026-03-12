using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI;

public class InstituicaoRepository : IInstituicaoRepository
{
    private readonly EventContext _context;
    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }
    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var InstituicaoBuscada = _context.Instituicaos.Find(id);

        if (InstituicaoBuscada != null)
        {
            InstituicaoBuscada.NomeFantasia = String.IsNullOrWhiteSpace(instituicao.NomeFantasia)? InstituicaoBuscada.NomeFantasia : instituicao.NomeFantasia;
            InstituicaoBuscada.Cnpj = String.IsNullOrWhiteSpace(instituicao.Cnpj)? InstituicaoBuscada.Cnpj : instituicao.Cnpj;
            InstituicaoBuscada.Endereco = String.IsNullOrWhiteSpace(instituicao.Endereco) ? InstituicaoBuscada.Endereco : instituicao.Endereco;
            _context.SaveChanges();
        }
    }

    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }

    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var InstituicaoBuscada = _context.Instituicaos.Find(id);
        if (InstituicaoBuscada != null)
        {
            _context.Instituicaos.Remove(InstituicaoBuscada);
            _context.SaveChanges();
        }
    }

    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.OrderBy(Instituicao => Instituicao.NomeFantasia).ToList();
    }
}
