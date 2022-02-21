using Dokument_AK.Entities;
using Dokument_AK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Data
{
    public interface IDokumentRepository
    {
        List<DokumentEnt> GetDokuments(string ZavodniBroj=null);

        DokumentEnt GetDokumentByID(Guid dokumentID);

        DokumentConfirmation CreateDokument(DokumentEnt dokument);

        void UpdateDokument(DokumentEnt dokument);

        void  DeleteDokument(Guid dokumentID);

        bool SaveChanges();
    }
}
