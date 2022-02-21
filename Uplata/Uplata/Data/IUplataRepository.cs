using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;
using Uplata.Models;

namespace Uplata.Data
{
    public interface IUplataRepository
    {
        List<Entities.Uplata> GetUplate();

        Entities.Uplata GetUplataByID(Guid uplataID);

        UplataConfirmationDto CreateUplata(Entities.Uplata uplata);

        UplataConfirmationDto UpdateUplata(Entities.Uplata uplata);

        void DeleteUplata(Guid uplataID);

        bool SaveChanges();
    }
}
