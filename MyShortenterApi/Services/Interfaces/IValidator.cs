using MyShortenterApi.Dtos;
using System.Threading.Tasks;

namespace MyShortenterApi.Services.Interfaces
{
    public interface IValidator<TRequest>
    {
        Task<ValidationResultDto> Validate(TRequest request);
    }
}