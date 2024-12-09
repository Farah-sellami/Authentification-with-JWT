using JobExpressBack.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JobExpressBack.Models.Repositories
{
    ////Ce service contient toute la logique métier pour l'authentification 
    ///et la generation des tokens.
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
       

        public AuthRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        public async Task<string> Register(RegisterModel model)
        {
            // Vérifiez si l'email existe déjà
            if (await userManager.FindByEmailAsync(model.Email) != null)
            {
                return "Email is already registered!";
            }

            // Créez un nouvel utilisateur
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Telephone = model.Telephone,
                PhotoProfile = model.PhotoProfile
            };

            var result = await userManager.CreateAsync(user, model.Password);

            // Vérifiez si la création a échoué
            if (!result.Succeeded)
            {
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }
            var role = string.IsNullOrEmpty(model.Role) ? "Client" : model.Role;
            // Ajoutez un rôle par défaut "Client" si nécessaire
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
            await userManager.AddToRoleAsync(user, role);

            return "Registration successful!";
        }

        public async Task<object> Login(LoginModel model)
        {
            // Vérifier si l'utilisateur existe
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                return new { Message = "Invalid email or password!" };
            }

            // Générer un token JWT
            var token = GenerateJwtToken(user);

            //update les activités utilisateur
            user.LastLoginDate = DateTime.UtcNow;
            await userManager.UpdateAsync(user);

            return new
            {
                Message = "Login successful!",
                Token = token,
                User = new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.FirstName,
                    user.LastName
                }
            };
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = configuration.GetSection("JWT");
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        };

            // Ajouter les rôles de l'utilisateur
            var roles = userManager.GetRolesAsync(user).Result;
            claims.AddRange(roles.Select(role => new Claim("roles", role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
