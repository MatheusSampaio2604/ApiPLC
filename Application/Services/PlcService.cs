using Application.Services.Interfaces;
using Domain.Models;
using Domain.Repository.Interface;
using S7.Net;

namespace Application.Services
{
    public class PlcService : InterPlcService
    {
        private readonly IPlcSettingsJsonRepository _plcSettingsRepository;
        private Plc _plc;

        public PlcService(IPlcSettingsJsonRepository plcSettingsRepository)
        {
            _plcSettingsRepository = plcSettingsRepository;
            UpdatePlcSettings();
        }

        private void UpdatePlcSettings()
        {
            PlcSettings settings = _plcSettingsRepository.GetSettingsPlc();

            CpuType cpuType = (CpuType)Enum.Parse(typeof(CpuType), settings.CpuType, true);
            _plc = new Plc(cpuType, settings.Ip1, settings.Rack, settings.Slot);
        }

        private async Task EnsureConnectedAsync()
        {
            if (!_plc.IsConnected)
                await ConnectAsync();
        }

        public async Task ConnectAsync()
        {
            Disconnect();
            UpdatePlcSettings();

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            try
            {
                var connectTask = Task.Run(() => _plc.Open(), cts.Token);
                var completedTask = await Task.WhenAny(connectTask, Task.Delay(Timeout.Infinite, cts.Token));

                if (completedTask == connectTask)
                {
                    await connectTask;
                }
                else
                {
                    throw new TimeoutException("The connection to the PLC has timed out at 15 seconds.");
                }
            }
            finally
            {
                cts.Dispose();
            }
        }


        public void Disconnect()
        {
            if (_plc.IsConnected)
                _plc.Close();
        }

        public async Task<T?> ReadAsync<T>(string addressplc)
        {
            await EnsureConnectedAsync();
            return await Task.Run(() => (T?)_plc.Read(addressplc));
        }

        public async Task<bool> WriteAsync<T>(string addressplc, T value)
        {
            try
            {
                await EnsureConnectedAsync();
                await Task.Run(() => _plc.Write(addressplc, value));
                return true;
            }
            catch { return false; }
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
