using LicnostService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Data
{
    public interface ILicnostRepository
    {

        List<Licnost> GetLicnosti(string funkcija = null);

        Licnost GetLicnostById(Guid licnostId);
        Licnost CreateLicnost(Licnost licnost);
        Licnost UpdateLicnost(Licnost licnost);
        void DeleteLicnost(Guid licnostId);

    }
}
