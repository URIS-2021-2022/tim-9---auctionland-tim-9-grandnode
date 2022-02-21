using Kupac_SK.Entities;
using Kupac_SK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Data
{
    public interface IKupacRepository
    {
        List<KupacModel> GetKupci();

        KupacModel GetKupacById(Guid kupacId);

        KupacModel CreateKupac(KupacModel kupacModel);

        void UpdateKupac(KupacModel kupacModel);

        void DeleteKupac(Guid kupacId);

        bool SaveChanges();

    }
}
