

namespace ECMDomain.Abstraction;
public class UserAlreadyExistException(
    List<string> errors) : Exception
    //, IBadRequest
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "DuplicateUser.Error",
        ErrorMessages = errors
    };
}
