using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TipoUsuarioController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoUsuarioRepository { get; set; }

        public TipoUsuarioController()
        {
            _tipoUsuarioRepository = new TipoUsuarioRepository();
        }

        [Authorize(Roles = "2")]
        [HttpGet]
        public IEnumerable<TipoUsuarioDomain> Listar()
        {
            return _tipoUsuarioRepository.ListarTipoUsuario();
        }

        [HttpPost]
        public IActionResult Cadastrar(TipoUsuarioDomain novoTipoUsuario)
        {
            _tipoUsuarioRepository.CadastrarTipoUsuario(novoTipoUsuario);
            return StatusCode(201);
        }

        [Authorize(Roles = "2")]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            TipoUsuarioDomain tipoUsuarioProcurado = _tipoUsuarioRepository.BuscarPorId(id);

            if (tipoUsuarioProcurado == null)
            {
                return NotFound("Nenhum usuario encontrado");

            }

            return Ok(tipoUsuarioProcurado);
        }

        [Authorize(Roles = "2")]
        [HttpPut]
        public IActionResult Atualizar(TipoUsuarioDomain tipoUsuarioAtualizado)
        {
            TipoUsuarioDomain usuarioProcurado = _tipoUsuarioRepository.BuscarPorId(tipoUsuarioAtualizado.IdTipoUsuario);

            if (usuarioProcurado != null)
            {
                try
                {
                    _tipoUsuarioRepository.AtualizarTipoUsuario(tipoUsuarioAtualizado);

                    return NoContent();
                }
                catch (Exception error)
                {
                    return BadRequest(error);
                }
            }

            return NotFound("Não rolou :(");
        }

        [Authorize(Roles = "2")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _tipoUsuarioRepository.DeletarTipoUsuario(id);

            return Ok("Usuário deletado");
        }
    }
}