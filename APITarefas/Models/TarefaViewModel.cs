using System.Collections.Generic;
using Tarefas.Models;

namespace Tarefas.Models
{
    public class TarefaViewModel
    {
            public IEnumerable<TarefaItem> TarefaItens {get; set;}
    }
}