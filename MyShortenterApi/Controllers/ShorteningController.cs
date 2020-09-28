using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyShortenterApi.Extensions;
using MyShortenterApi.Services.Command;
using System.Threading.Tasks;

namespace MyShortenterApi.Controllers
{
    [ApiController]
    public class ShorteningController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShorteningController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/shorten")]
        public async Task<IActionResult> ShortenUrl(ShortenUrlCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToResponse();
        }
    }
}