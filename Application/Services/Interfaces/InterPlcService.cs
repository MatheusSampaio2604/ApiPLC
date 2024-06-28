using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface InterPlcService
    {
        Task<bool> ConnectAsync(); 
        void Disconnect();
        Task<T?> ReadAsync<T>(string addressplc);
        Task<bool> WriteAsync<T>(string addressplc, T value);
        Task StartStop(string addressplc);

    }
}
