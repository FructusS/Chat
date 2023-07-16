using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AvaloniaChat.Application.DTO.Auth;
using AvaloniaChat.Application.DTO.Message;
using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Desktop.Events;
using ExCSS;
using System.Net.Http.Json;
using System.Text.Json;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Desktop.Models;

namespace AvaloniaChat.Desktop.Services
{
    public class UserService
    {
        private const string baseUrl = "http://localhost:5000/api/User";
        private readonly HubConnection _hubConnection;
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(baseUrl),
        };

        public async Task<BaseResponse<UserDto?>> RegistrationUser(CreateUserDto user)
        {
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(user),
            Encoding.UTF8,
                "application/json");
            try
            {
                using HttpResponseMessage response = await _httpClient.PostAsync($"{baseUrl}/registration", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<UserDto>();
                    if (authResponse != null)
                    {
                        return new BaseResponse<UserDto?>("", false, authResponse) { Data = authResponse, IsError = false, ErrorText = ""};
                    }
                }
                else
                {
                    return new BaseResponse<UserDto?>("some thing went wrong", true, null) { Data = null, IsError = false, ErrorText = "" };
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }


    }
}
