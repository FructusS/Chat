using AvaloniaChat.Backend.Models.Login;
using AvaloniaChat.Backend.Models.Registration;
using Microsoft.AspNetCore.Mvc;

namespace AvaloniaChat.Backend.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> LoginUser(LoginRequest loginRequest);
        Task<RegistrationResponse> RegistrationUser(RegistrationRequest registrationRequest);
    }
}
