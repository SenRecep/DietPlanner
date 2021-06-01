﻿
using DietPlanner.DTO.Response;
using DietPlanner.Shared.ExtensionMethods;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.Server.BLL.ExtensionMethods
{
    public static class ResponseExtensionMethods
    {
        public static IActionResult CreateResponseInstance<T>(this Response<T> response)
        {
            if (response.IsNull())
            {
                return new ObjectResult(Response<T>.Fail(
                    statusCode: StatusCodes.Status500InternalServerError,
                    isShow: false,
                    path: "bilinmiyor",
                    errors: "beklenmedik hata"
                    ))
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode == StatusCodes.Status204NoContent ? StatusCodes.Status200OK : response.StatusCode
            };
        }
    }
}
