using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinhasTarefasAPI.Models;
using MinhasTarefasAPI.Models.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepository _tarefaRepository;
        private UserManager<ApplicationUser> _userManager;

        public TarefaController(ITarefaRepository tarefaRepository, UserManager<ApplicationUser> userManager)
        {
            _tarefaRepository = tarefaRepository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("sicronizar")]
        public ActionResult Sicronizar([FromBody] List<Tarefa> tarefas)
        {
            return Ok(_tarefaRepository.Sicronizacao(tarefas));
        }

        [Authorize]
        [HttpGet("restaurar")]
        public ActionResult Restauracao(DateTime data)
        {
            var usuario = _userManager.GetUserAsync(HttpContext.User).Result;
            return Ok(_tarefaRepository.Restauracao(usuario, data));
        }

    }
}
