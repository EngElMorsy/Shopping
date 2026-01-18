

namespace ECMDomain.Abstraction;
public class InternalServerException(
    string errorCode,
    List<string> errors) : Exception
    //, IINternalServerError
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = errorCode,
        ErrorMessages = errors
    };
}
