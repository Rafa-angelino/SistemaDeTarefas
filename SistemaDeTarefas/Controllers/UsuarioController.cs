using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuario;

        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> BuscarTodosUsuarios()
        {
           List<Usuario> usuarios = await _usuario.BuscarTodosUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> BuscarPorId(int id)
        {
            Usuario usuario = await _usuario.BuscarPorId(id);
            return Ok(usuario);
        }

        [HttpPost]

        public async Task<ActionResult<Usuario>> Cadastrar([FromBody] Usuario usuarioModel)
        {
            Usuario usuario = await _usuario.Adicionar(usuarioModel);
            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Atualizar([FromBody] Usuario usuarioModel, int id)
        {
            usuarioModel.Id = id;
            Usuario usuario = await _usuario.Atualizar(usuarioModel, id);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Usuario>> Apagar(int id)
        {
            bool apagado = await _usuario.Apagar(id);
            return Ok(apagado);
        }




    }
}
