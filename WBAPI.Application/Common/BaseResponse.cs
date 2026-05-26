namespace WBAPI.Application.Common
{
    public record BaseResponse<T>(bool Success, string Message, T? Data = default);
}
