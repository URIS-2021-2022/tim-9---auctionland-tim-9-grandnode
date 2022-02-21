using KomisijaService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.ServiceCalls
{
    public interface ILicnostService
    {
        Task<LicnostDto> LicnostKomisije(Guid licnostId);
    }
}
