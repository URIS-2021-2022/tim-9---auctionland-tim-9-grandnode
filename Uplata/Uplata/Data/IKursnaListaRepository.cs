using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;
using Uplata.Models;

namespace Uplata.Data
{
    public interface IKursnaListaRepository
    {
        List<KursnaLista> GetKursneListe();

        KursnaLista GetKursnaListaByID(Guid kursnaListaID);

        KursnaListaConfirmationDto CreateKursnaLista(KursnaLista kursnaLista);

        KursnaListaConfirmationDto UpdateKursnaLista(KursnaLista kursnaLista);

        void DeleteKursnaLista(Guid kursnaListaID);

        bool SaveChanges();
    }
}
