

namespace ECMDomain.Abstraction;
public class InvalidTokenException(
    List<string> errors) : Exception
    //, IBadRequest
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "InvalidToken.Error",
        ErrorMessages = errors
    };
}
