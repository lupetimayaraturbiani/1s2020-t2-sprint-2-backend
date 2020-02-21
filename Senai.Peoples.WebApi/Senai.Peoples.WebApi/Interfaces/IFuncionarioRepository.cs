using Senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface IFuncionarioRepository
    {
        /// Listar todos os funcionarios
        List<FuncionarioDomain> Listar();

        /// Cadastrar funcionario
        void Cadastrar(FuncionarioDomain funcionario);
    }
}
