


using ECMDomain.Abstraction;

namespace ECMDomain.Exceptions;

public class BadRequestException(
    List<string> errors) : Exception
  {
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "BadRequest.Error",
        ErrorMessages = errors
    };
  } 
