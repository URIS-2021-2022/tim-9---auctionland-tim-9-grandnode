using Dokument_AK.Entities;
using Dokument_AK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Data
{
    public interface IEksterniDokumentRepository
    {
        List<EksterniDokumentEnt> GetEksterniDokumentEnts(bool Izmenjen=false);
        EksterniDokumentEnt GetEskterniDokumentByID(Guid dokumentID);

        EksterniDokumentConfirmation CreateEksterniDokument(EksterniDokumentEnt dokumentID);

        void UpdateEksterniDokument(EksterniDokumentEnt dokumentID);

        void DeleteEksterniDokument(Guid dokumentID);

        bool SaveChanges();
    }
}
