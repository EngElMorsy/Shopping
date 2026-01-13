using ECMDomain.Abstraction;
namespace ECMDomain.Exceptions;
public class NullObjectException(
    List<string> errors) : Exception
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "NullObject.Error",
        ErrorMessages = errors
    };
}
