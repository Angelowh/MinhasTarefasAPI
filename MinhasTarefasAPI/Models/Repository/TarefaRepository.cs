
using MinhasTarefasAPI.Database;
using MinhasTarefasAPI.Models.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Models.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly MinhasTarefasContext _banco;

        public TarefaRepository(MinhasTarefasContext banco)
        {
            _banco = banco;
        }

        public List<Tarefa> Restauracao(ApplicationUser usuario, DateTime dataUltimaSicronizacao)
        {
            var query = _banco.Tarefas.Where(a => a.UsuarioId == usuario.Id).AsQueryable();
            if (dataUltimaSicronizacao != null)
            {
                query.Where(a => a.Criado >= dataUltimaSicronizacao || a.Atualizado >= dataUltimaSicronizacao);
            }
            return query.ToList<Tarefa>();
        }

        public List<Tarefa> Sicronizacao(List<Tarefa> tarefas)
        {
            var tarefasNovas = _banco.Tarefas.Where(a => a.IdTarefaAPI == 0);
            if (tarefasNovas.Count() > 0)
            {
                foreach (var tarefa in tarefasNovas)
                {
                    _banco.Tarefas.Add(tarefa);
                }
            }
            var tarefasExcluidasAtualizadas = _banco.Tarefas.Where(a => a.IdTarefaAPI == 0);
            if (tarefasExcluidasAtualizadas.Count() > 0)
            {
                foreach (var tarefa in tarefasExcluidasAtualizadas)
                {
                    _banco.Tarefas.Update(tarefa);
                }
            }
            _banco.SaveChanges();
            return tarefasNovas.ToList();
        }
    }
}
