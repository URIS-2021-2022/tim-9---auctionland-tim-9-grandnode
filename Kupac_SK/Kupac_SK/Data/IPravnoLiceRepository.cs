using Kupac_SK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Data
{
   public interface IPravnoLiceRepository
    {
        List<PravnoLice> getPravnaLica();
        PravnoLice GetPravnoLiceById(Guid plID);
        PravnoLice CreatePravnoLice(PravnoLice pravnoLice);
        void DeletePravnoLice(Guid pravnoLice);
        void UpdatePravnoLice(PravnoLice pravnoLice);
        bool SaveChanges();
    }
}
