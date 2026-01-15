namespace ECMDomain.Abstraction;
public interface ILoggable
{
    public bool IsNotSuccessfull { get; set; }
    //Use LogContext USing PushProperty
    public Error? Errors { get; set; }

}
