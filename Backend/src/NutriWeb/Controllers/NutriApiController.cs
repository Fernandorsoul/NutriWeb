using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutriWeb.Interfaces;
using NutriWeb.Models;

namespace NutriWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        // Model de resposta padrão
        private ActionResult ResponseHandler(bool success, string message, object data = null)
        {
            return Ok(new
            {
                success = success,
                message = message,
                data = data
            });
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return ResponseHandler(true, "Usuários obtidos com sucesso.", users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter usuários: {ex.Message}");
                return StatusCode(500, ResponseHandler(false, "Erro ao obter usuários."));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound(ResponseHandler(false, $"Usuário com ID {id} não encontrado."));
                }
                return ResponseHandler(true, "Usuário obtido com sucesso.", user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter usuário com ID {id}: {ex.Message}");
                return StatusCode(500, ResponseHandler(false, $"Erro ao obter usuário com ID {id}."));
            }
        }

        [HttpPost]
        [Authorize] // Placeholder para autenticação
        public async Task<ActionResult> CreateUser([FromBody] UserModel user)
        {
            // Validação de dados
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseHandler(false, "Dados inválidos.", ModelState));
            }

            try
            {
                var createdUser = await _userRepository.CreateAsync(user, user.Id, user.Email);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, ResponseHandler(true, "Usuário criado com sucesso.", createdUser));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao criar usuário: {ex.Message}");
                return StatusCode(500, ResponseHandler(false, "Erro ao criar usuário."));
            }
        }

        [HttpPut("{id}")]
        [Authorize] // Placeholder para autenticação
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserModel user)
        {
            if (id != user.Id)
            {
                return BadRequest(ResponseHandler(false, "ID do usuário não coincide."));
            }

            // Validação de dados
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseHandler(false, "Dados inválidos.", ModelState));
            }

            try
            {
                var updatedUser = await _userRepository.UpdateAsync(user, id);
                return Ok(ResponseHandler(true, "Usuário atualizado com sucesso.", updatedUser));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar usuário com ID {id}: {ex.Message}");
                return StatusCode(500, ResponseHandler(false, $"Erro ao atualizar usuário com ID {id}."));
            }
        }

        [HttpDelete("{id}")]
        [Authorize] // Placeholder para autenticação
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var deletedUser = await _userRepository.DeleteAsync(id);
                if (deletedUser == null)
                {
                    return NotFound(ResponseHandler(false, $"Usuário com ID {id} não encontrado."));
                }
                return Ok(ResponseHandler(true, "Usuário excluído com sucesso.", deletedUser));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir usuário com ID {id}: {ex.Message}");
                return StatusCode(500, ResponseHandler(false, $"Erro ao excluir usuário com ID {id}."));
            }
        }
    }
}
