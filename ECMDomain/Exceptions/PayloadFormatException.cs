using ECMDomain.Abstraction;
namespace ECMDomain.Exceptions;
public class PayloadFormatException(
    List<string> errors) : Exception
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "PayloadFormat.Error",
        ErrorMessages = errors
    };
}