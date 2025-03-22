using Newtonsoft.Json;
namespace api;
public static class ErrorResponseFormatter
{
    public static string FormatErrorResponse(string errorMessage)
    {
        var errorResponse = new
        {
            error = true,
            message = errorMessage
        };

        return JsonConvert.SerializeObject(errorResponse);
    }
}
