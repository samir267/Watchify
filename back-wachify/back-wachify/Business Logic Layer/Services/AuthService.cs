using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Data;
using back_wachify.Data.Model;
using back_wachify.Data_Layer.Repositroy;
using back_wachify.Dto;
using back_wachify.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace back_wachify.Business_Logic_Layer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserRepository _userRepository;


        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext, IUserRepository userRepository)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _userRepository = userRepository;



        }


        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("role", user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public void SetRefreshToken(User user, RefreshToken newRefreshToken)
        {
            var response = _httpContextAccessor.HttpContext.Response;

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            // Update user's refresh token in the database
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            _dbContext.SaveChanges();
        }

        public RefreshToken GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            // Update user's refresh token in the database
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;
            _dbContext.SaveChanges();

            return refreshToken;
        }

        public async Task<User> RegisterUser(UserDto request)
        {
            var existingUser = await _userRepository.FindByUsername(request.Username);
            if (existingUser != null)
            {
                throw new ArgumentException("This username is already taken. Please choose a different one.");
            }
            // Generate password hash and salt
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = request.Role,
                ConfirmationCode = GenerateRandomCode(),
                IsEmailConfirmed = false,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
            };

            try
            {
                _userRepository.add(user);

                return user;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
                {
                    throw new ArgumentException("This username is already taken. Please choose a different one.");
                }
                else
                {
                    throw new Exception("An error occurred while registering the user.", ex);
                }
            }
        }
        private const string EncryptionKey = "AESKey1234567890"; // Change this to your own secret key

        public static string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aesAlg.IV = new byte[16]; // Initialization vector (IV) is required for AES encryption

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                            csEncrypt.FlushFinalBlock();
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aesAlg.IV = new byte[16]; // Initialization vector (IV) is required for AES encryption

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
        public async Task<User> RegisterUserSocial(SocialregisterDto request)
        {
            var existingUser = await _userRepository.FindByUsername(request.Username);
            if (existingUser != null)
            {
                throw new ArgumentException("This username is already taken. Please choose a different one.");
            }
            Random random = new Random();
            int randomNumber = random.Next(100000000, 1000000000);

            // Generate password hash and salt
            CreatePasswordHash(randomNumber.ToString(), out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = request.Role,
                ConfirmationCode = GenerateRandomCode(),
                IsEmailConfirmed = false,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                facebookId= request.facebookId,
                googleId= request.googleId,
                provider=request.provider,




            };

            try
            {
                _userRepository.add(user);

                return user;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
                {
                    throw new ArgumentException("This username is already taken. Please choose a different one.");
                }
                else
                {
                    throw new Exception("An error occurred while registering the user.", ex);
                }
            }
        }
        public async Task<(string Token, bool IsPasswordWrong,Role role)> login(AuthDto request)
        {
            var user = await _userRepository.FindByUsername(request.Username);
            Console.WriteLine("usernnnnnn", request);

            if (user == null)
            {
                Console.WriteLine("email ghalet");

                return (Token: null, IsPasswordWrong: false,role:0);
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {

                return (Token: null, IsPasswordWrong: true, role: 0);
            }

            string token = CreateToken(user);
            Console.WriteLine("token", token);

            var refreshToken = GenerateRefreshToken(user);
            SetRefreshToken(user, refreshToken);

            return (Token: token, IsPasswordWrong: false,role:user.Role);
        }
        public async Task<(string Token, bool IsSocialNotLogged)> loginSocial(SocialAuthDto request)
        {
            var user = await _userRepository.FindByUsername(request.Username);
            Console.WriteLine("usernnnnnn", request);

            if (user == null)
            {

                return (Token: null, IsSocialNotLogged: false);
            }

            if (request.socialId!= user.facebookId )
            {

                return (Token: null, IsSocialNotLogged: true);
            }

            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken(user);
            SetRefreshToken(user, refreshToken);

            return (Token: token, IsSocialNotLogged: false);
        }
        public async Task<(string Token, string Message)> RefreshToken(string refreshToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);

            if (user == null)
            {
                return (null, "Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return (null, "Token expired."); // Token expired
            }

            // Generate new JWT token
            string token = CreateToken(user);

            // Generate and set new refresh token
            var newRefreshToken = GenerateRefreshToken(user);
            SetRefreshToken(user, newRefreshToken);

            return (token, null); // Token refreshed successfully
        }


        public string GenerateRandomCode()
        {
            Random random = new Random();
            int code = random.Next(00000, 99999); // Génère un nombre aléatoire entre 10000 et 99999


            return code.ToString();
        }




    }
}
