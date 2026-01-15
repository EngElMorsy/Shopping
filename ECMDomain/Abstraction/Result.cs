
using System.Text.Json.Serialization;

namespace ECMDomain.Abstraction;
 
//** Befor Use IResult
//public class Result<TDto> where TDto : BaseEntity
//** After Use IResult 
//**and Before Use Ilogger
//public class Result<TDto> 
//**After Use Ilogger
public class Result<TDto> : ILoggable where TDto : IResult
{
    //Success
    private Result( TDto? data,int statusCode)
    {
        Data = data;
        IsNotSuccessfull = false;
        StatusCode = statusCode;
    }

    //Success without data
    private Result(int statusCode)
    {
        IsNotSuccessfull = false;
        StatusCode = statusCode;
    }

    //Fail with one error
    private Result( int statusCode, string errorCode, string errorMessage)
    {
        IsNotSuccessfull = true;
        StatusCode = statusCode;
        Errors = new()
        {
            //**Before Use Error class
            // {errorCode,errorMessage } 
            ErrorCode = errorCode,
            ErrorMessages = [errorMessage]
        };
        
       
     
    }

    //Fail with Many error 
    //**Before Use Error Class
    //private Result(
    // int statusCode,
    //Dictionary<string,string> errors)
    //{
    //    IsNotSuccessfull = true;
    //    StatusCode = statusCode;
    //    Errors = errors;
    //}
    private Result(int statusCode, Error errors)
    {
        IsNotSuccessfull = true;
        StatusCode = statusCode;
        Errors = errors;
    }

    public Result() { }

    public TDto? Data { get; set; }

    [JsonIgnore]
    public bool IsNotSuccessfull { get; set; }

    public int StatusCode { get; set; }
    //**Before Use Error Class
    //public Dictionary<string,string>? Errors { get; set; }
    public Error? Errors { get; set; }


    public static Result<TDto> Success( TDto data,int statusCode)   => new(data, statusCode);

    public static Result<TDto> Success(int statusCode)    => new(statusCode);

    public static Result<TDto> Failed( int statusCode, string errorCode, string errorMessage)
        => new(statusCode, errorCode, errorMessage);
    
    
    
    //**Before Use ERROR Class
    //public static Result<TDto> Failed(
    //   int statusCode,
    //   Dictionary<string,string> errors)
    //   => new(statusCode, errors);


    public static Result<TDto> Failed(int statusCode,Error errors)=> new(statusCode, errors);
}

//** THis Class For ErrorCollectio Or 
//**If Error Have One Or Many Errors 
//**instead of Dictionary Generic
public class Error
{
    public string ErrorCode { get; set; } = null!;
    public List<string> ErrorMessages { get; set; } = null!;
}

//** Befor Use IResult
//public class NoContentDto; 
//** After Use IResult
public class NoContentDto : IResult;

