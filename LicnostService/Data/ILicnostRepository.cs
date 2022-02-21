using LicnostService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Data
{
    public interface ILicnostRepository
    {

        bool SaveChanges();
        List<Licnost> GetLicnosti(string funkcija = null);

        Licnost GetLicnostById(Guid licnostId);
        Licnost CreateLicnost(Licnost licnost);
        void UpdateLicnost(Licnost licnost);
        void DeleteLicnost(Guid licnostId);

    }
}
