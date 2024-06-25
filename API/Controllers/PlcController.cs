﻿using Application.Services;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using S7.Net.Types;

namespace API.Controllers
{
    /// <summary>
    /// OBS.: Todas as requisições tentam executar automaticamente uma conexão com o servidor!
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
            object data = Empty;

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
                        break;

                    case "double":
                        if (request.Value is double doubleValue) data = doubleValue;
                        else if (double.TryParse(request.Value?.ToString(), out double parsedDouble)) data = parsedDouble;
                        break;

                    
                    default:
                        return BadRequest("Unsupported type.");
                }

                var result = await _plcService.WriteAsync(request.AddressPlc, data);
                return Ok(result);
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














        /// <summary>
        /// List all plcs in archive
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetListPlc")]
        public IActionResult GetListPlc()
        {
            try
            {
                var plcConfigureds = _interJsonService.LoadItem();
                if (plcConfigureds == null || !plcConfigureds.Any())
                    return NotFound();
                
                return Ok(plcConfigureds);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Save informations in json archive
        /// </summary>
        /// <param name="plcConfigured"></param>
        /// <returns></returns>
        [HttpPost("SaveInListPlc")]
        public IActionResult Save([FromBody] List<PlcConfigured> plcConfigured)
        {
            try
            {
                _interJsonService.SaveItem(plcConfigured);

                return Ok("Success!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}/updateTag")]
        public IActionResult UpdateTag(int id, [FromBody] PlcConfigured plc)
        {
            try
            {
                _interJsonService.UpdateTag(id, plc);
                return Ok("Tag address updated successfully.");
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


        //[HttpPut("{id}/UpdateSettingsPlc")]
        //public IActionResult UpdateSettingsPlc(int id, [FromBody] PlcSettings settings)
        //{
        //    try
        //    {
        //        _interJsonService.UpdateTag(id, settings);
        //        return Ok("Tag address updated successfully.");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
















    }

}
