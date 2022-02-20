using Dokument_AK.Entities;
using Dokument_AK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Data
{
    public interface IInterniDokumentRepository
    {
        List<InterniDokumentEnt> GetInterniDokumentEnts(bool Izmenjen=false);
        InterniDokumentEnt GetInterniDokumentByID(Guid dokumentID);

        InterniDokumentConfirmation CreateInterniDokument(InterniDokumentEnt dokument);

        void UpdateInterniDokument(InterniDokumentEnt dokument);

        void DeleteInterniDokument(Guid dokumentID);

        bool SaveChanges();
    }
}
