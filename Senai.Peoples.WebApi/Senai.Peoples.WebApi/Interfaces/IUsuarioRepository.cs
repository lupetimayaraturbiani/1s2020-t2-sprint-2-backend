using Senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface IUsuarioRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns>Retorna um usuario validado
        UsuarioDomain BuscarPorEmailSenha(string email, string senha);

        /// Listar usuarios
        List<UsuarioDomain> Listar();

        /// Cadastrar usuarios
        void Cadastrar(UsuarioDomain usuario);

        /// Atualizar usuario
        void Atualizar(UsuarioDomain usuario);

        /// Buscar usuario por id
        UsuarioDomain BuscarPorId(int id);

        /// Deletar usuario
        void Deletar(int id);


    }
}
