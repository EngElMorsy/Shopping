namespace ECEMCore.Abstraction.Caching;
public interface ICacheService
{ 
    // Get Data Form Redis
    Task<T?> GetAsync<T>(
        string key,
        CancellationToken cancellationToken = default);

    //Set Data To Redis
    Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default);
   
    //Remove Data From Redis
    Task RemoveAsync(
        string key,
        CancellationToken cancellationToken = default);
}
