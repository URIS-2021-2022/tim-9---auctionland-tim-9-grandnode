using Dokument_AK.Entities;
using Dokument_AK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Data
{
    public interface IStatusDokumentaRepository
    {
        List<StatusDokumentaEnt> GetStatusDokumentaEnts(bool Usvojen=false);
        StatusDokumentaEnt GetStatusDokumentaByID(Guid statusDokID);

        StatusDokumentaConfirmation CreateStatusDokument(StatusDokumentaEnt dokument);
        void  UpdateStatusDokumenta(StatusDokumentaEnt dokument);

        void DeleteStatusDokumenta(Guid statusDokID);

        bool SaveChanges();
    }
}
