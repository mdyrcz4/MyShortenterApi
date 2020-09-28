using MediatR;
using MyShortenterApi.Dtos;
using MyShortenterApi.Repositories.Interfaces;
using MyShortenterApi.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MyShortenterApi.Services.Query
{
    public class GetUrlByKeyQuery : IRequest<CQRSResponseDto<string>>
    {
        public string Key { get; set; }
    }

    public class GetUrlByKeyQueryValidator : IValidator<GetUrlByKeyQuery>
    {
        public Task<ValidationResultDto> Validate(GetUrlByKeyQuery request)
        {
            if (string.IsNullOrWhiteSpace(request.Key))
                return Task.FromResult(ValidationResultDto.Error("A key is required."));

            return Task.FromResult(ValidationResultDto.Success);
        }
    }

    public class GetUrlByKeyQueryHandler : IRequestHandler<GetUrlByKeyQuery, CQRSResponseDto<string>>
    {
        private readonly IRepository _repository;

        public GetUrlByKeyQueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<CQRSResponseDto<string>> Handle(GetUrlByKeyQuery query, CancellationToken cancellationToken)
        {
            var url = await _repository.GetUrl(query.Key);
            if (string.IsNullOrWhiteSpace(url))
                return CQRSResponseDto.NotFound<string>($"{query.Key} does not exist.");
            else
                return CQRSResponseDto.Success(url);
        }
    }
}