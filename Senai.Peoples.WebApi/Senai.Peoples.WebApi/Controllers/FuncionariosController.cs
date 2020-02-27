using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class FuncionariosController : ControllerBase
    {
        private IFuncionarioRepository _funcionarioRepository { get; set; }

        public FuncionariosController()
        {
            _funcionarioRepository = new FuncionarioRepository();
        }

        [HttpGet]
        public IEnumerable<FuncionarioDomain> Get()
        {
            return _funcionarioRepository.Listar();
        }

        [HttpPost]
        public IActionResult Post(FuncionarioDomain novoFuncionario)
        {
            _funcionarioRepository.Cadastrar(novoFuncionario);
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            FuncionarioDomain funcionarioProcurado = _funcionarioRepository.BuscarPorId(id);

            if (funcionarioProcurado == null)
            {
                return NotFound("Nenhum funcionario encontrado");

            }

            return Ok(funcionarioProcurado);
        }

        [HttpPut]
        public IActionResult Put(FuncionarioDomain funcionarioAtualizado)
        {
            FuncionarioDomain funcionarioProcurado = _funcionarioRepository.BuscarPorId(funcionarioAtualizado.IdFuncionario);

            if (funcionarioProcurado != null)
            {
                try
                {
                    _funcionarioRepository.Atualizar(funcionarioAtualizado);

                    return NoContent();
                }
                catch (Exception error)
                {
                    return BadRequest(error);
                }
            }

            return NotFound("Não rolou :(");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _funcionarioRepository.Deletar(id);

            return Ok("Funcionário deletado");
        }

        [HttpGet("{Nome/Catarina}")]
        public IActionResult BuscarPorNome(string Nome)
        {
            FuncionarioDomain funcionarioProcurado = _funcionarioRepository.BuscarPorNome(Nome);

            if (funcionarioProcurado == null)
            {
                return NotFound("Nenhum funcionario encontrado");

            }

            return Ok(funcionarioProcurado);
        }

    }
}