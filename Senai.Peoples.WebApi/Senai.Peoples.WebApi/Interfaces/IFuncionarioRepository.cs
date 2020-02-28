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

        /// Listar por id um  funcionario
        FuncionarioDomain BuscarPorId(int id);

        /// Atualizar funcionario
        void Atualizar(FuncionarioDomain funcionario);

        /// Deletar funcionario
        void Deletar(int id);

        /// Buscar por nome do funcionario
        FuncionarioDomain BuscarPorNome(string Nome);

        /// Listar por nome completo
        List<FuncionarioDomain> ListarPorNomeCompleto();

        /// Listar ordenado
        List<FuncionarioDomain> ListarOrdenado(string ordem); 
    }
}
