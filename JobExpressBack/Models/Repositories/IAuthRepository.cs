using JobExpressBack.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JobExpressBack.Models.Repositories
{
    public interface IAuthRepository
    {
        Task<string> Register(RegisterModel model);
        Task<object> Login(LoginModel model);
        
    }
}
