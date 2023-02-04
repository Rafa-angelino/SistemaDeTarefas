using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class TarefaRepositorio : ITarefa
    {
        private readonly TarefasDbContext _dbContext;

        public TarefaRepositorio(TarefasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tarefa> BuscarPorId(int id)
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);  
        }

        public async Task<List<Tarefa>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .ToListAsync();
        }
        
        public async Task<Tarefa> Adicionar(Tarefa tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();
            return tarefa;
        }

        public async Task<Tarefa> Atualizar(Tarefa tarefa, int id)
        {
            Tarefa tarefaPorId = await BuscarPorId(id);
            if(tarefaPorId == null)
            {
                throw new Exception($"Nã foi possível encontrar nenhuma tarefa com o id {id}");
            }
            tarefaPorId.Descricao = tarefa.Descricao;
            tarefaPorId.Nome = tarefa.Nome;
            tarefaPorId.Status = tarefa.Status; 
            tarefaPorId.UsuarioId = tarefa.UsuarioId;

            _dbContext.Tarefas.Update(tarefaPorId);
            await _dbContext.SaveChangesAsync();
            return tarefaPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            Tarefa tarefaPorId = await BuscarPorId(id);
            if (tarefaPorId == null)
            {
                throw new Exception($"Nã foi possível encontrar nenhuma tarefa com o id {id}");
            }
            _dbContext.Remove(tarefaPorId);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
