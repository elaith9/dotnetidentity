using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using TestDotNetCoreTemplate.Models.Dto;
using TestTemplate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace TestDotNetCoreTemplate.Repositories
{
    public class AuthRepository
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly SymmetricSecurityKey _securityKey;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthRepository(IConfiguration config, IMapper mapper, SymmetricSecurityKey securityKey, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _config = config;
            _mapper = mapper;
            _securityKey = securityKey;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<User> Login(UserCredentialsDto creds)
        {
            var user = await _userManager.FindByNameAsync(creds.Username);
            var result = await _signInManager.CheckPasswordSignInAsync(user, creds.Password, false);

            if (result.Succeeded) return user;
            return null;
        }

        public async Task<bool> Register(UserRegistrationDto userToRegister)
        {
            var user = _mapper.Map<User>(userToRegister);
            var result = await _userManager.CreateAsync(user, userToRegister.Password);

            if (result.Succeeded) return true;
            return false;
        }

        public async Task<bool> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded) return true;
            return false;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var creds = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(TimeSpan.Parse(_config.GetSection("AppSettings:TokenExpiration").Value)),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}