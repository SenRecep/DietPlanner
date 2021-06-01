using System;

using DietPlanner.DTO.Interfaces;

namespace DietPlanner.DTO.Auth
{
    public record AuthModel(Guid UserId, string Role):IDTO { }
}
