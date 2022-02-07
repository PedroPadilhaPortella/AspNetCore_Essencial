using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tarefas.Models;




namespace Tarefas.Services
{
    //class concreta implementa a classe de inteerface
    public class TempTarefaItemService : ITarefaItemService
    {
      public Task<IEnumerable<TarefaItem>> GetItemAsync()
      {
          //retornar o array de itens
          IEnumerable<TarefaItem> itens = new []
          {
                new TarefaItem
                {
                    
                    Nome = "BIA",
                    EstaCompleta = false,
                    DataConclusao = DateTimeOffset.Now.AddDays(30)
                },
                new TarefaItem
                {
                    
                    Nome = "pietter",
                    EstaCompleta = false,
                    DataConclusao = DateTimeOffset.Now.AddDays(30)
                }
          };
          return Task.FromResult(itens);

      }
    }
}