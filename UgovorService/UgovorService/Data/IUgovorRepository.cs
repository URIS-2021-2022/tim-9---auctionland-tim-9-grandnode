using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;

namespace UgovorService.Data
{
    public interface IUgovorRepository
    {

        List<UgovorEnt> GetUgovors(string ZavodniBr = null);

        UgovorEnt GetUgovorByID(Guid ugovorID);

        UgovorConfirmation CreateUgovor(UgovorEnt ugovor);

        void UpdateUgovor(UgovorEnt ugovor);

        void DeleteUgovor(Guid ugovorID);

        bool SaveChanges();
    }
}
