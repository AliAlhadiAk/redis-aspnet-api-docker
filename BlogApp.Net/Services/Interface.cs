namespace BlogApp.Net.Services
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        bool setData<T>(string key,T value,DateTimeOffset expirationTime);
        object RemoveData(string key);
    }
}
