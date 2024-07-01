using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface InterConfigureOptionsService
    {
        ConfigureOptionsWrapper? GetAllConfigureOptions();
        List<Drivers>? GetDriversOptions();
        List<CpuTypes>? GetCpuTypesOptions();
    }
}
