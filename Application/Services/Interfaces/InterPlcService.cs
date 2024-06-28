namespace Application.Services.Interfaces
{
    public interface InterPlcService
    {
        Task ConnectAsync();
        void Disconnect();
        Task<T?> ReadAsync<T>(string addressplc);
        Task<bool> WriteAsync<T>(string addressplc, T value);
        Task StartStop(string addressplc);

    }
}
