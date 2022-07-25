using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Models.Repository.Contracts
{
    public interface ITarefaRepository
    {
        List<Tarefa> Sicronizacao(List<Tarefa> tarefas);
        List<Tarefa> Restauracao(ApplicationUser usuario, DateTime dataUltimaSicronizacao);
    }
}
