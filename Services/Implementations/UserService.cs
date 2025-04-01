using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using OuathAuthentication.Entities;
using OuathAuthentication.Models;
using OuathAuthentication.Repositories.Interfaces;
using OuathAuthentication.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OuathAuthentication.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public async Task<UserResponseModel> LoginUser(LoginRequestModel request)
        {
            var userExists = await _userRepository.CheckIfUserExist(request.Email);
            if (!userExists)
            {
                return null;
            }

            var getUser = await _userRepository.GetUser(request.Email);
            getUser.LastLoginAt = DateTime.UtcNow;

            
            bool checkPassword = BCrypt.Net.BCrypt.Verify(request.Password, getUser.Password);
            if (!checkPassword)
            {
                return null;
            }

         
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{getUser.FirstName} {getUser.LastName}"),
              new Claim(ClaimTypes.Role, getUser.Role),
               new Claim(ClaimTypes.Email, getUser.Email),
              new Claim(ClaimTypes.NameIdentifier, getUser.Id.ToString())
               };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // Token expires in 2 hours
                signingCredentials: creds
            );

            var userResponse = new UserResponseModel
            {
                Email = getUser.Email,
                FirstName = getUser.FirstName,
                LastName = getUser.LastName,
                Role = getUser.Role,
                LastLoginAt = getUser.LastLoginAt,
                TimesOfLogin = getUser.TimesOfLogin++,
                Token = new JwtSecurityTokenHandler().WriteToken(token) 
            };

            return userResponse;
        }


        public async Task<UserResponseModel> RegisterUser(UserRequestModel request)
        {
            var userExists = await _userRepository.CheckIfUserExist(request.Email);
            if (userExists)
            {
                return null;
            }
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password,salt);

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = hashedPassword,
                Role = "User"
            };

            var createdUser = await _userRepository.AddAsync(newUser);
            if(createdUser != null)
            {
                var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, createdUser.FirstName + " " + createdUser.LastName),
               new Claim(ClaimTypes.Role, createdUser.Role),
               new Claim(ClaimTypes.Email, createdUser.Email),
               new Claim(ClaimTypes.NameIdentifier, createdUser.Id.ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return new UserResponseModel
                {
                    FirstName = createdUser.FirstName,
                    LastName = createdUser.LastName,
                    Email = createdUser.Email,
                    Role = createdUser.Role,
                    Token = tokenString
                };
            }

            return null;  
        }

    }
}
