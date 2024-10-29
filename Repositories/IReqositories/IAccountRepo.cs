using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Models.Dtos;
using Store.Models.Models;

namespace Store.Repositories.IReqositories;

public interface IAccountRepo
{
    public Task<GeneralResponse> RegisterAsync(DtoNewUser registerDTO);
    public Task<GeneralResponse> LoginAsync(DtoLogin loginDTO);
    public Task<GeneralResponse> RefreshTokenAsync(DtoRefreshToken request);
    public Task<GeneralResponse> ChangePasswordAsync(DtoResetPassword dto);
    public Task<GeneralResponse> LogOutAsync(int userId);
    public GeneralResponse RevokeAllTokens(string userId);
}