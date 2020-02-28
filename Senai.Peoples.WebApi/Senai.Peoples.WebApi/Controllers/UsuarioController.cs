using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Produces ("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpGet]
        public IEnumerable<UsuarioDomain> Listar()
        {
            return _usuarioRepository.Listar();
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioDomain novoUsuario)
        {
            _usuarioRepository.Cadastrar(novoUsuario);
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            UsuarioDomain usuarioProcurado = _usuarioRepository.BuscarPorId(id);

            if (usuarioProcurado == null)
            {
                return NotFound("Nenhum usuario encontrado");

            }

            return Ok(usuarioProcurado);
        }


        [HttpPut]
        public IActionResult Atualizar(UsuarioDomain usuarioAtualizado)
        {
            UsuarioDomain usuarioProcurado = _usuarioRepository.BuscarPorId(usuarioAtualizado.IdUsuario);

            if (usuarioProcurado != null)
            {
                try
                {
                    _usuarioRepository.Atualizar(usuarioAtualizado);

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
            _usuarioRepository.Deletar(id);

            return Ok("Usuário deletado");
        }


        [HttpPost("login")]
        public IActionResult Post(UsuarioDomain login)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorEmailSenha(login.Email, login.Senha);

            if (usuarioBuscado == null)
            {
                return NotFound("E-mail ou senha inválidos");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString()),
                new Claim("Claim Personalizada", "Valor teste")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("peoples-chave-autenticacao"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Peoples.WebApi",
                audience: "Peoples.WebApi",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });


        }
    }
}