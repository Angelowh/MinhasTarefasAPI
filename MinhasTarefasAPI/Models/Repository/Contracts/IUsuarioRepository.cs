using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefasAPI.Models.Repository.Contracts
{
    public interface IUsuarioRepository
    {
        void Cadastrar(ApplicationUser usuario, string senha);
        ApplicationUser Obter(string email, string senha);
    }
}
