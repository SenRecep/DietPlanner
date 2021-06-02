﻿using System.Threading.Tasks;

using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Response;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface IAuthenticationService
    {
        UserDto User { get; }
        Task Initialize();
        Task<Response<UserDto>> Login(string identityNumber, string password);
        Task Logout();
        bool IsAuthorize { get; }
    }
}
