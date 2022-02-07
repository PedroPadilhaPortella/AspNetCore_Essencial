using System;


namespace Tarefas.Models
{
    public class TarefaItem
    {
        //atalho prop + tab
        public System.Guid Id { get; set; }
        public bool EstaCompleta { get; set; }
        public string Nome { get; set; }
       public DateTimeOffset? DataConclusao { get; set; }

    }
}