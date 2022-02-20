using KomisijaService.Entities;
using KomisijaService.Models.Komisija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Data.Interfaces
{
    public interface IKomisijaRepository
    {
        List<Komisija> GetAllKomisija(Guid? predsednikId = null);
        Komisija GetKomisijaById(Guid komisijaId);
        KomisijaConfirmationDto CreateKomisija(Komisija komisija);
        void UpdateKomisija(Komisija komisija);
        void DeleteKomisija(Guid komisijaId);
        bool SaveChanges();
    }
}
