using galic_korisnik.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Data
{
    public interface ITipKorisnikaRepository
    {
        List<TipKorisnika> GetTipKorisnikaList();
        TipKorisnika GetTipKorisnikaById(Guid tipKorisnikaId);
        TipKorisnika CreateTipKorisnika(TipKorisnika tipKorisnika);
        void UpdateTipKorisnika(TipKorisnika tipKorisnika);
        void DeleteTipKorisnika(Guid tipKorisnikaId);
        bool SaveChanges();
    }
}
