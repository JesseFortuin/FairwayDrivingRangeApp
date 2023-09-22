namespace FairwayDrivingRange.Shared.Dtos
{
    public class ApiResponseDto<T>
    {
        public bool IsSuccess { get; private set; }

        public T? Value { get; private set; }

        public string ErrorMessage { get; private set; }
     
        public ApiResponseDto(T value)
        {
            IsSuccess = true;

            Value = value;
        }

        public ApiResponseDto()
        {
            
        }

        public static ApiResponseDto<T> Error(string errorMessage)
        {
            return new ApiResponseDto<T>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }
    }
}