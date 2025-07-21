using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Common
{
    public class Result<TEntityViewModel>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public TEntityViewModel? Data { get; set; }
        public string? AccessToken { get; set; }

        public static Result<TEntityViewModel> LoginSuccess(TEntityViewModel data, string accessToken) => new() { Success = true, Data = data, AccessToken = accessToken };

        public static Result<TEntityViewModel> LoginFail(string message) => new() { Success = false, Message = message };

        public static Result<TEntityViewModel> Ok() => new() { Success = true, Message = "Success" };

        public static Result<TEntityViewModel> Ok(TEntityViewModel data) => new() { Success = true, Data = data };

        public static Result<TEntityViewModel> Ok(string message) => new() { Success = true, Message = message };

        public static Result<TEntityViewModel> Ok(string message, TEntityViewModel data) => new() { Success = true, Message = message, Data = data };

        public static Result<TEntityViewModel> Fail(string message) => new() { Success = false, Message = message };
    }
}
