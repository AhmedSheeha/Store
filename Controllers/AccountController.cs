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
using Microsoft.AspNetCore.Authorization;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountRepo AccountRepo { get; set; }
        public AccountController(IAccountRepo accountRepo)
        {
            this.AccountRepo = accountRepo;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(DtoNewUser user)
        {
            if (ModelState.IsValid)
            {
                GeneralResponse generalResponse = await AccountRepo.RegisterAsync(user);
                if (generalResponse.IsSuccess == true)
                {
                    return Ok(generalResponse.Data);
                }
                else
                {
                    foreach (var item in generalResponse.Data)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(DtoLogin login)
        {
            if (ModelState.IsValid)
            {
                GeneralResponse generalResponse = await AccountRepo.LoginAsync(login);
                if(generalResponse.IsSuccess) {
                   
                        return Ok(generalResponse.Data);
                    }
                    else return Unauthorized();
                }
            return BadRequest(ModelState);
        }
    }
}