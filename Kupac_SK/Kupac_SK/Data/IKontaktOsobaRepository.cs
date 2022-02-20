using Kupac_SK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Data
{
   public interface IKontaktOsobaRepository
    {
        List<KontaktOsobaModel> GetKontaktOsobe();
        //ko- kontakt osoba 
        KontaktOsobaModel GetKontaktOsobaById(Guid koId);
        KontaktOsobaModel CreateKontaktOsoba(KontaktOsobaModel kontaktOsoba);
        void UpdateKontaktOsoba(KontaktOsobaModel kontaktOsoba);
        void DeleteKontaktOsoba(Guid koId); 




    }
}
