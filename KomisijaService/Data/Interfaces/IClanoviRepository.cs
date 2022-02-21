using KomisijaService.Entities;
using KomisijaService.Models.Clanovi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Data.Interfaces
{
    public interface IClanoviRepository
    {
        List<Clanovi> GetAllClanovi(Guid? komisijaId = null);
        Clanovi GetClanoviById(Guid clanoviId);
        ClanoviConfirmationDto CreateClanovi(Clanovi clanovi);
        void UpdateClanovi(Clanovi clanovi);
        void DeleteClanovi(Guid clanoviId);
        bool SaveChanges();
    }
}
