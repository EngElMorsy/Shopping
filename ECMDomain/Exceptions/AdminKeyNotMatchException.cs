

using MediatR;

namespace ECMDomain.Abstraction;
public class AdminKeyNotMatchException(
    List<string> errors) : Exception
    //, IBaseRequest
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "AdminKey.Error",
        ErrorMessages = errors
    };
}
