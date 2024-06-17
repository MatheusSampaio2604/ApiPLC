﻿using API.Models;
using Microsoft.Extensions.Options;
using S7.Net;

namespace API.Services
{
    public class PlcService
    {
        private readonly Plc _plc;

        public PlcService(IOptions<PlcSettings> plcSettings)
        {
            var settings = plcSettings.Value;
            var cpuType = (CpuType)Enum.Parse(typeof(CpuType), settings.CpuType, true);
            _plc = new Plc(cpuType, settings.Ip1, settings.Rack, settings.Slot);
        }

        private async Task EnsureConnectedAsync()
        {
            if (!_plc.IsConnected)
               await Task.Run(() => _plc.Open());
        }

        public async Task ConnectAsync()
        {
           await Task.Run(() => _plc.Open());
        }

        public void Disconnect()
        {
            _plc.Close();
        }

        public async Task<T?> ReadAsync<T>(string addressplc)
        {
            await EnsureConnectedAsync();
            return await Task.Run(() => (T?)_plc.Read(addressplc));
        }

        public async Task WriteAsync<T>(string addressplc, T value)
        {
            await EnsureConnectedAsync();
            await Task.Run(() => _plc.Write(addressplc, value));
        }

        public async Task StartStop(string addressplc)
        {
            await EnsureConnectedAsync();
            bool currentValue = await ReadAsync<bool>(addressplc);
            bool newValue = !currentValue;
            await WriteAsync(addressplc, newValue);
        }
    }
}
