using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyShortenterApi.Extensions;
using MyShortenterApi.Services.Query;
using System.Threading.Tasks;

namespace MyShortenterApi.Controllers
{
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RedirectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/{key}")]
        public async Task<IActionResult> RedirectUrl(string key)
        {
            var result = await _mediator.Send(new GetUrlByKeyQuery { Key = key });
            if (result.IsSuccess)
                return Redirect(result.Data);
            else
                return result.ToResponse();
        }
    }
}