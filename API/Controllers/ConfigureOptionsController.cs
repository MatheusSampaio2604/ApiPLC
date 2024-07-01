using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigureOptionsController : ControllerBase
    {
        private readonly InterConfigureOptionsService _optionsService;

        public ConfigureOptionsController(InterConfigureOptionsService optionsService)
        {
            _optionsService = optionsService;
        }

        [HttpGet("GetAllConfigureOptions")]
        public IActionResult GetAllConfigureOptions()
        {
            try
            {
                return Ok(_optionsService.GetConfigureOptions());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetDriversOptions")]
        public IActionResult GetDriversOptions()
        {
            try
            {
                return Ok(_optionsService.GetDriversOptions());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCpuTypesOptions")]
        public IActionResult GetCpuTypesOptions()
        {
            try
            {
                return Ok(_optionsService.GetCpuTypesOptions());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}