using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Business_Logic_Layer.Services;
using back_wachify.Data.Model;
using back_wachify.Dto;
using Microsoft.AspNetCore.Mvc;

namespace back_wachify.Presentation_Layer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
       

        public UserController(IUserService userService)
        {
            _userService = userService;
           

        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, "Une erreur interne du serveur s'est produite.");
            }
        }

        [HttpGet("getByRole/{role}")]
        public async Task<IActionResult> GetUsersByRole(Role role)
        {
            try
            {
                var users = await _userService.GetUsersByRoleAsync(role);
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, "Une erreur interne du serveur s'est produite.");
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, "Une erreur interne du serveur s'est produite.");
            }
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(id, userDto);
                if (updatedUser == null)
                {
                    return NotFound();
                }
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, "Une erreur interne du serveur s'est produite.");
            }
        }

        [HttpPost("deactivate/{id}")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            try
            {
                var result = await _userService.DeactivateUserAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, "Une erreur interne du serveur s'est produite.");
            }
        }




        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                if (result)
                {
                    return Ok(new { message = "Utilisateur supprimé avec succès" });
                }
                else
                {
                    return NotFound(new { message = "Utilisateur non trouvé" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, new { message = "Une erreur s'est produite lors de la suppression de l'utilisateur." });
            }
        }




        [HttpPost("envoyerEmail")]
        public async Task<IActionResult> EnvoyerEmail([FromBody] emailDto emailDto)
        {
            try
            {
                await _userService.SendEmailAsync(emailDto, emailDto.RecipientEmail); // Attendre la fin de l'exécution de la méthode asynchrone
                return Ok("Email envoyé avec succès");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception: {ex}");
                return BadRequest("Erreur lors de l'envoi de l'email.");
            }
        }

        [HttpPost("verifierCode")]
        public async Task<IActionResult> VerifierCode(int code,string email)
        {
            try
            {
                bool codeCorrect = await _userService.VerifierCodeAsync(code, email);

                if (codeCorrect)
                {
                    return Ok("Code correct. Opération réussie.");
                }
                else
                {
                    return BadRequest("Code incorrect. Veuillez réessayer.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception: {ex}");
                return BadRequest("Erreur lors de la vérification du code.");
            }
        }

    }
}
