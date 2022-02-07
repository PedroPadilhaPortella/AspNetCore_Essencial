using System.Collections.Generic;
using System.Threading.Tasks;
using Tarefas.Models;

namespace Tarefas.Services
{
    public interface ITarefaItemService
    {
        //Uma tarefa que contem uma lista que contem os itens de tarefa
        Task<IEnumerable<TarefaItem>> GetItemAsync();
        
    }
}