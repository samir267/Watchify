using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Data.Model;
using back_wachify.Data_Layer.Repositroy;
using back_wachify.Dto;
using back_wachify.Model;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace back_wachify.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        //private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            //_emailService = emailService;
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        }
        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }


        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                return await _userRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                throw; // Re-throw the exception to be caught in the calling method
            }
        }

        public async Task<List<User>> GetUsersByRoleAsync(Role role)
        {
            try
            {
                return await _userRepository.GetByRoleAsync(role);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                throw; // Re-throw the exception to be caught in the calling method
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                return await _userRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                throw; // Re-throw the exception to be caught in the calling method
            }
        }

        public async Task<User> UpdateUserAsync(int id, UserDto userDto)
        {
            try
            {
                return await _userRepository.UpdateAsync(id, userDto);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                throw; // Re-throw the exception to be caught in the calling method
            }
        }

        public async Task<bool> DeactivateUserAsync(int id)
        {
            try
            {
                return await _userRepository.DeactivateAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                throw; // Re-throw the exception to be caught in the calling method
            }
        }


        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                return await _userRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                throw; // Re-throw the exception to be caught in the calling method
            }
        }












        public async Task SendEmailAsync(emailDto emailDto, string recipient)
        {
            Random random = new Random();
            int codeAleatoire = random.Next(100000, 999999);

            try
            {
                bool codeAssigned = await _userRepository.AffecterCode(recipient, codeAleatoire);


                if (codeAssigned)
                {
                    // If the code was successfully assigned, proceed to send the email
                    await EnvoyerEmailAsync(emailDto, recipient, codeAleatoire); // Call the local email sending method
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");
                throw; // Re-throw the exception to be caught in the calling method
            }



        }
        private async Task EnvoyerEmailAsync(emailDto emailDto, string recipient, int codeAleatoire)
        {
            using (var client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587; // Port SMTP de Gmail pour TLS
                client.EnableSsl = true;

                // Configurez les informations d'authentification
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("samirchemkhi09@gmail.com", "qxwu aljj klff vqvc");

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("samirchemkhi09@gmail.com"),
                    Subject = codeAleatoire.ToString(),
                    Body = "Vous Venez De Recevoir Votre Code De Récupération ",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(recipient);

                try
                {
                    client.Send(mailMessage);
                    Console.WriteLine("Email envoyé avec succès");
                }
                catch (SmtpException ex)
                {
                    // Log the SMTP exception
                    Console.WriteLine($"Erreur SMTP : {ex.StatusCode} - {ex.Message}");
                    throw; // Re-throw the exception to be caught in the calling method
                }
            }
        }


        public async Task<bool> VerifierCodeAsync(int codeVerification, string email)
        {
            try
            {
                int? secretCode = await _userRepository.GetSecretCodeByEmailAsync(email);

                if (secretCode.HasValue && secretCode == codeVerification)
                {
                    await _userRepository.ConfirmedEmail(email);

                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                Console.WriteLine($"Exception: {ex}");

                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    Console.WriteLine($"Inner Exception: {ex}");
                }

                throw new Exception("Erreur lors de la vérification du code.", ex);
            }
        }

        
    }

    /*public async Task<bool> VerifyCodeAsync(int codeVerification, string email)
    {
        try
        {
            return await _userRepository.VerifyCodeAsync(codeVerification, email);
        }
        catch (Exception ex)
        {
            // Log the exception and handle it appropriately
            Console.WriteLine($"Exception: {ex}");
            throw; // Re-throw the exception to be caught in the calling method
        }
    }*/

}
    
