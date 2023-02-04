using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefa _tarefa;

        public TarefaController(ITarefa tarefa)
        {
            _tarefa = tarefa;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<Tarefa>>> BuscarTodasTarefas()
        {
           List<Tarefa> tarefa = await _tarefa.BuscarTodasTarefas();
            return Ok(tarefa);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> BuscarPorId(int id)
        {
            Tarefa tarefa = await _tarefa.BuscarPorId(id);
            return Ok(tarefa);
        }

        [HttpPost]

        public async Task<ActionResult<Tarefa>> Cadastrar([FromBody] Tarefa tarefaModel)
        {
            Tarefa tarefa = await _tarefa.Adicionar(tarefaModel);
            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Tarefa>> Atualizar([FromBody] Tarefa tarefaModel, int id)
        {
            tarefaModel.Id = id;
            Tarefa tarefa = await _tarefa.Atualizar(tarefaModel, id);
            return Ok(tarefa);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Tarefa>> Apagar(int id)
        {
            bool apagado = await _tarefa.Apagar(id);
            return Ok(apagado);
        }




    }
}
