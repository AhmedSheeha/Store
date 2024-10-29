using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store.Repositories.IReqositories;
using Store.Models.Dtos;
using Store.Models.Models;

namespace Store.Repositories
{
    public class AccountRepository : IAccountRepo
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountRepository(UserManager<AppUser> userManager, IAppDbContext appDbContext, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public Task<GeneralResponse> ChangePasswordAsync(DtoResetPassword dto)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> LoginAsync(DtoLogin loginDTO)
        {
            GeneralResponse generalResponse = new GeneralResponse();  
            var user = await _userManager.FindByNameAsync(loginDTO.UserName);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                {

                    generalResponse = await GenerateJwtToken(user);
                    return generalResponse;
                }
            }
            generalResponse.IsSuccess = false;
            return generalResponse;
        }

        public Task<GeneralResponse> LogOutAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> RefreshTokenAsync(DtoRefreshToken request)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> RegisterAsync(DtoNewUser registerDTO)
        {
            GeneralResponse response = new GeneralResponse();
            var ExistingUser = await _userManager.FindByNameAsync(registerDTO.userName);
            if (ExistingUser != null)
            {
                response.IsSuccess = false;
                response.Data = "this Email is Already Existing";
                return response;
            }
            AppUser user = new AppUser
            {
                Email = registerDTO.email,
                UserName = registerDTO.userName,
                EmailConfirmed = false
            };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "USER");
                response.IsSuccess = true;
                response.Data = "Registered Successfully";
                return response;
            }
            response.IsSuccess = false;
            response.Data = result.Errors.Select(er => er.Description).ToList();
            return response;
        }

        public GeneralResponse RevokeAllTokens(string userId)
        {
            throw new NotImplementedException();
        }
        private string GenerateRefereshToken()
        {
            return "d";
        }
        private async Task<GeneralResponse> GenerateJwtToken(AppUser user)
        {
            GeneralResponse generalResponse = new GeneralResponse();
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddHours(1),
                signingCredentials: sc
                );
            var _token = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,

            };
            generalResponse.IsSuccess = true;
            generalResponse.Data = _token;
            return generalResponse;
        }
    }
}
