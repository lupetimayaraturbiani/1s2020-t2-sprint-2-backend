﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [Authorize(Roles = "2")]
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

        [Authorize(Roles = "2")]
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

        [Authorize(Roles = "2")]
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

        [Authorize(Roles = "2")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _usuarioRepository.Deletar(id);

            return Ok("Usuário deletado");
        }


       
    }
}