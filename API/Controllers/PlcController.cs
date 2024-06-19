using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// OBS.: Todas as requisições tentam executar automaticamente uma conexão com o servidor!
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlcController : ControllerBase
    {
        private readonly PlcService _plcService;

        public PlcController(PlcService plcService)
        {
            _plcService = plcService;
        }

        /// <summary>
        /// Inicia uma conexão com o PLC
        /// </summary>
        /// <returns></returns>
        [HttpGet("connect")]
        public async Task<IActionResult> Connect()
        {
            await _plcService.ConnectAsync();
            return Ok("Connected to PLC");
        }

        /// <summary>
        /// Encerra a conexão com o PLC
        /// </summary>
        /// <returns></returns>
        [HttpGet("disconnect")]
        public IActionResult Disconnect()
        {
            _plcService.Disconnect();
            return Ok("Disconnected from PLC");
        }

        /// <summary>
        /// Executa a leitura de um valor no PLC
        /// </summary>
        /// <param name="addressplc"></param>
        /// <returns></returns>
        [HttpGet("read/{addressplc}")]
        public async Task<IActionResult> Read(string addressplc)
        {
            var result = await _plcService.ReadAsync<object>(addressplc);
            return Ok(result);
        }

        /// <summary>
        /// Executa a escrita de um novo valor no PLC
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("write")]
        public async Task<IActionResult> Write(WriteRequest request)
        {
            object data;

            try
            {
                switch (request.Type.ToLower())
                {
                    case "bool":
                        if (request.Value is bool boolValue) data = boolValue;
                        else if (bool.TryParse(request.Value?.ToString(), out bool parsedBool)) data = parsedBool;
                        else return BadRequest("Invalid value type for bool.");
                        break;
                    case "int":
                        if (request.Value is int intValue) data = intValue;
                        else if (int.TryParse(request.Value?.ToString(), out int parsedInt)) data = parsedInt;
                        else return BadRequest("Invalid value type for int.");
                        break;

                    case "double":
                        if (request.Value is double doubleValue) data = doubleValue;
                        else if (double.TryParse(request.Value?.ToString(), out double parsedDouble)) data = parsedDouble;
                        else return BadRequest("Invalid value type for double.");
                        break;

                    // Adicione outros tipos conforme necessário
                    default:
                        return BadRequest("Unsupported type.");
                }

                await _plcService.WriteAsync(request.AddressPlc, data);
                return Ok("Write successful");
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        /// <summary>
        /// Alterna entre os valores 0 e 1
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("startstop")]
        public async Task<IActionResult> StartStop(StartStopRequest request)
        {
            await _plcService.StartStop(request.AddressPlc);
            return Ok("Toggled value successfully");
        }
    }

}
