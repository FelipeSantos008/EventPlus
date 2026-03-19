using EventPlus.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly IComentarioEventoRepository _comentarioEventoRepository;
    public ComentarioEventoController(IComentarioEventoRepository comentarioEventoRepository)
    {
        _comentarioEventoRepository = comentarioEventoRepository;
    }


}
