using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// NOTE: All requests automatically attempt to connect to the server!
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlcController : ControllerBase
    {
        private readonly InterPlcService _plcService;
        private readonly InterJsonService _interJsonService;

        public PlcController(InterPlcService plcService, InterJsonService interJsonService)
        {
            _plcService = plcService;
            _interJsonService = interJsonService;
        }

        /// <summary>
        /// Starts a connection to the PLC
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestConnectionPlc")]
        public async Task<IActionResult> Connect()
        {
            try
            {
                await _plcService.ConnectAsync();

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Closes the connection with the PLC
        /// </summary>
        /// <returns></returns>
        [HttpGet("disconnect")]
        public IActionResult Disconnect()
        {
            try
            {
                _plcService.Disconnect();
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }
        }

        /// <summary>
        /// Reads a value on the PLC
        /// </summary>
        /// <param name="addressplc"></param>
        /// <returns></returns>
        [HttpGet("read/{addressplc}")]
        public async Task<IActionResult> Read(string addressplc)
        {
            
            return Ok(await _plcService.ReadAsync<object>(addressplc));
        }

        /// <summary>
        /// Writes a new value to the PLC
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("write")]
        public async Task<IActionResult> Write(List<WriteRequest> requests)
        {
            object data = Empty;
            bool result = false;

            if (requests.Count == 0)
                return BadRequest("Request cannot be null.");

            try
            {
                foreach (var request in requests)
                {
                    switch (request.Type.ToLower())
                    {
                        case "bool":
                            if (bool.TryParse(request.Value?.ToString(), out bool parsedBool)) data = parsedBool;
                            break;
                        case "int":
                            if (int.TryParse(request.Value?.ToString(), out int parsedInt)) data = parsedInt;
                            break;
                        case "double":
                            if (double.TryParse(request.Value?.ToString(), out double parsedDouble)) data = parsedDouble;
                            break;
                        default:
                            return BadRequest("Unsupported type.");
                    }
                    result = await _plcService.WriteAsync(request.AddressPlc, data);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Toggles between values ​​0 and 1
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("startstop")]
        public async Task<IActionResult> StartStop(StartStopRequest request)
        {
            try
            {
                await _plcService.StartStop(request.AddressPlc);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }
        }

        /// <summary>
        /// List all plcs in archive
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetListPlc")]
        public IActionResult GetListPlc()
        {
            try
            {
                return Ok(_interJsonService.LoadItem());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Save informations in Plc list archive
        /// </summary>
        /// <param name="plcConfigured"></param>
        /// <returns></returns>
        [HttpPost("SaveInListPlc")]
        public IActionResult Save([FromBody] List<PlcsInUse> plcConfigured)
        {
            try
            {
                _interJsonService.SaveItem(plcConfigured);

                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }
        }

        /// <summary>
        /// Update tag in list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="plc"></param>
        /// <returns></returns>
        [HttpPut("{id}/updateTag")]
        public IActionResult UpdateTag(int id, PlcsInUse plc)
        {
            try
            {
                _interJsonService.UpdateTag(id, plc);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }
        }

        [HttpDelete("{id}/deleteTagList")]
        public IActionResult DeleteTag(int id)
        {
            try
            {
                _interJsonService.DeleteTagInList(id);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        [HttpPost("UpdateSettingsPlc")]
        public IActionResult UpdateSettingsPlc([FromBody] PlcSettings settings)
        {
            try
            {
                _plcService.Disconnect();
                _interJsonService.UpdateSettingsPlc(settings);
                return Ok(true);
            }
            catch
            {
                return Ok(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSettingsPlc")]
        public IActionResult GetSettingsPlc()
        {
            try
            {
                return Ok(_interJsonService.GetSettingsPlc());
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
