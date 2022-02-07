using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Services;
using System.Threading.Tasks;
using Tarefas.Models;




namespace Tarefas.Controllers
{
    public class TarefasController : Controller
    {
        //injecao de dependencia
        ITarefaItemService _tarefaService;
        public TarefasController(ITarefaItemService tarefaService)
        {
            _tarefaService = tarefaService;
        }
        //LISTA DE TAREFAS
        public async Task<IActionResult> Index()
        {

            //baixo acomplamento
            //TempTarefaItemService servico = new TempTarefaItemService();          
            var tarefas = await _tarefaService.GetItemAsync();

            var model = new TarefaViewModel();
            {
                    model.TarefaItens = tarefas;
            }

            return View(model);
        }
    }
}