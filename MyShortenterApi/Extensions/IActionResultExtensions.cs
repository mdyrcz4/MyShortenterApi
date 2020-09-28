using Microsoft.AspNetCore.Mvc;
using MyShortenterApi.Dtos;

namespace MyShortenterApi.Extensions
{
    public static class IActionResultExtensions
    {
        public static IActionResult ToResponse(this CQRSResponseDto response)
        {
            if (response.IsUnsuccessful)
                return new ObjectResult(new { response.ErrorMessage }) { StatusCode = response.StatusCode };
            else if (response.HasData)
                return new ObjectResult(new { Data = response.GetData() }) { StatusCode = response.StatusCode };
            else
                return new StatusCodeResult(response.StatusCode);
        }
    }
}