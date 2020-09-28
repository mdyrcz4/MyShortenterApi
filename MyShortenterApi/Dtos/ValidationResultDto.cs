namespace MyShortenterApi.Dtos
{
    public class ValidationResultDto
    {
        public bool IsSuccess => string.IsNullOrWhiteSpace(ErrorMessage);
        public string ErrorMessage { get; set; }

        public static ValidationResultDto Success => new ValidationResultDto();

        public static ValidationResultDto Error(string message) => new ValidationResultDto(message);

        public ValidationResultDto()
        {
        }

        public ValidationResultDto(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}