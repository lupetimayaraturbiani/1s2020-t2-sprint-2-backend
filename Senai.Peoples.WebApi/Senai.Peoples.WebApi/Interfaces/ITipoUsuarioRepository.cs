using Senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface ITipoUsuarioRepository
    {
        /// Listar Tipo de Usuarios
        List<TipoUsuarioDomain> ListarTipoUsuario();


        void CadastrarTipoUsuario(TipoUsuarioDomain tipoUsuario);


        TipoUsuarioDomain BuscarPorId(int id);


        void AtualizarTipoUsuario(TipoUsuarioDomain tipoUsuario);


        void DeletarTipoUsuario(int id);
    }
}
