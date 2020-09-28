using MediatR;
using MyShortenterApi.Dtos;
using MyShortenterApi.Repositories.Interfaces;
using MyShortenterApi.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyShortenterApi.Services.Command
{
    public class ShortenUrlCommand : IRequest<CQRSResponseDto>
    {
        public string Url { get; set; }
    }

    public class ShortenUrlCommandValidator : IValidator<ShortenUrlCommand>
    {
        public Task<ValidationResultDto> Validate(ShortenUrlCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Url))
                return Task.FromResult(ValidationResultDto.Error("A url is required."));

            Uri uriResult;
            bool isUrlValid = Uri.TryCreate(request.Url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (!isUrlValid)
                return Task.FromResult(ValidationResultDto.Error("Url is invalid."));

            return Task.FromResult(ValidationResultDto.Success);
        }
    }

    public class ShortenUrlCommandHandler : IRequestHandler<ShortenUrlCommand, CQRSResponseDto>
    {
        private readonly IRepository _repository;

        public ShortenUrlCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<CQRSResponseDto> Handle(ShortenUrlCommand command, CancellationToken cancellationToken)
        {
            var done = false;
            string key;
            do
            {
                key = GenerateRandomKey();

                if (await _repository.DoesKeyExist(key))
                    continue;

                done = true;
            } while (!done);

            await _repository.Add(key, command.Url);
            return CQRSResponseDto.Success(key);
        }

        private string GenerateRandomKey(int length = 6)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}