using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NutriWeb.Interfaces;
using NutriWeb.Models;

namespace NutriWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter usuários: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter usuário com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel user)
        {
            try
            {
                var createdUser = await _userRepository.CreateAsync(user, user.Id, user.Email);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar usuário: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> UpdateUser(int id, UserModel user)
        {
            try
            {
                var updatedUser = await _userRepository.UpdateAsync(user, id);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar usuário com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> DeleteUser(int id)
        {
            try
            {
                var deletedUser = await _userRepository.DeleteAsync(id);
                return Ok(deletedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir usuário com ID {id}: {ex.Message}");
            }
        }
    }
}