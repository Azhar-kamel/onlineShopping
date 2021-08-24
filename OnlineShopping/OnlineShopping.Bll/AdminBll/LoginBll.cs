using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShopping.Data.UnitOfWork;
using OnlineShopping.DTO;
using OnlineShopping.Entities;
using OnlineShopping.Shared.JWT;
using OnlineShopping.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Bll.AdminBll
{
    public class LoginBll : ILoginBll
    {
        private readonly UserManager<Admin> _UserManger;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IOptions<AppSettings> _AppSettings;
        public LoginBll(UserManager<Admin> UserManger, IUnitOfWork UnitOfWork, IOptions<AppSettings> AppSettings)
        {
            _UserManger = UserManger;
            _UnitOfWork = UnitOfWork;
            _AppSettings = AppSettings;
        }


        private List<Claim> GenerateClaims(Admin admin)
        {
            var claims = new List<Claim>
            {
                new Claim("id", admin.Id.ToString()),
                new Claim(ClaimTypes.Name, admin.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var adminRoles = _UserManger.GetRolesAsync(admin).GetAwaiter().GetResult();
            foreach (var role in adminRoles)
            {
                claims.Add(new Claim("roleName", role.ToString()));
            }
            return claims;
        }


        private string GenrateToken(Admin admin)
        {
            var claims = GenerateClaims(admin);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_AppSettings.Value.Secret));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return serializedToken;
        }


        private string getUserRole(Admin user)
        {
            
            var adminRoles = _UserManger.GetRolesAsync(user).GetAwaiter().GetResult();
            if (adminRoles.Count() > 0)
                return adminRoles[0];
            return "";
        }


        public ResultViewModel Authenticate(loginViewModel loginUser, Admin user)
        {
            ResultViewModel result = new ResultViewModel()
            {
                Succes = false,
                Message = "Invalid Password"
            };
            var checkPassword = _UserManger.CheckPasswordAsync(user, loginUser.password).GetAwaiter().GetResult();
            if (checkPassword)
            {
                string JWTToken = GenrateToken(user);
                string userRole = getUserRole(user);
                AuthenticatedUserDTO returnObject = new AuthenticatedUserDTO()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Token = JWTToken,
                    Role = userRole,
                };
                result.Succes = true;
                result.Message= "Logged in successfully";
                result.ReturnObject = returnObject;
            }
            return result;
        }
    }
}
