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

        [HttpGet("buscar/{Nome}")]
        public IActionResult BuscarPorNome(string Nome)
        {

            return Ok(_funcionarioRepository.BuscarPorNome(Nome));
        }

        [HttpGet("nomescompletos")]
        public IActionResult GetFullName()
        {
            // Faz a chamada para o método .ListarNomeCompleto            
            // Retorna a lista e um status code 200 - Ok
            return Ok(_funcionarioRepository.ListarPorNomeCompleto());
        }

        [HttpGet("ordenacao/{ordem}")]
        public IActionResult GetOrderBy(string ordem)
        {
            // Verifica se a ordenação atende aos requisitos
            if (ordem != "ASC" && ordem != "DESC")
            {
                // Caso não, retorna um status code 404 - BadRequest com uma mensagem de erro
                return BadRequest("Não é possível ordenar da maneira solicitada. Por favor, ordene por 'ASC' ou 'DESC'");
            }

            // Retorna a lista ordenada com um status code 200 - OK
            return Ok(_funcionarioRepository.ListarOrdenado(ordem));
        }





    }
}