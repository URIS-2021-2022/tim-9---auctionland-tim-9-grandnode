using Dokument_AK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Data
{
    public class DokumentMockRepository : IDokumentRepository
    {
       

        public static List<DokumentEnt> DokumentEnts { get; set; } = new List<DokumentEnt>();

        public DokumentMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            DokumentEnts.AddRange(new List<DokumentEnt>
            {
                new DokumentEnt
                {
                    DokumentID = Guid.Parse("1794d8c7-6c5c-4725-9d92-d819bdc07773"),
                    StatusDokID = Guid.Parse("f822c45b-2fd7-4fa6-98ec-64e31c0529e6"),
                    ZavodniBroj="15548/RS7",
                    Datum=DateTime.Parse("2021-11-15T09:00:00"),
                    DatumDonosenjaDokumenta=DateTime.Parse("2021-12-15T09:00:00")
                },
                new DokumentEnt
                {
                    DokumentID= Guid.Parse("cfe84b37-bb6d-498d-a546-5dee8758ed1a"),
                    StatusDokID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    ZavodniBroj="17748/RS7",
                    Datum=DateTime.Parse("2019-11-15T09:00:00"),
                    DatumDonosenjaDokumenta=DateTime.Parse("2019-12-15T09:00:00")
                }
            });
        }

        public DokumentEnt GetDokumentByID(Guid dokumentID)
        {
            return DokumentEnts.FirstOrDefault(D => D.DokumentID == dokumentID);
        }

        public List<DokumentEnt> GetDokuments(string ZavodniBroj=null)
        {
            return (from e in DokumentEnts
                    where string.IsNullOrEmpty(ZavodniBroj) || e.ZavodniBroj == ZavodniBroj
                    select e).ToList();
        }

         public DokumentConfirmation CreateDokument(DokumentEnt dokument)
        {
            dokument.DokumentID = Guid.NewGuid();
            dokument.StatusDokID= Guid.NewGuid();
            DokumentEnts.Add(dokument);
            var d = GetDokumentByID(dokument.DokumentID);

            return new DokumentConfirmation
            {
                DokumentID= d.DokumentID,
                StatusDokID= (Guid)d.StatusDokID,
                ZavodniBroj=d.ZavodniBroj,
                Datum=d.Datum,
                DatumDonosenjaDokumenta=d.DatumDonosenjaDokumenta
            };
        }

        public void DeleteDokument(Guid dokumentID)
        {
            DokumentEnts.Remove(DokumentEnts.FirstOrDefault(e => e.DokumentID== dokumentID));
        }

        public void UpdateDokument(DokumentEnt dokument)
        {
            DokumentEnt dok = GetDokumentByID(dokument.DokumentID);

            dok.DokumentID = dokument.DokumentID;
            dok.StatusDokID = dokument.StatusDokID;
            dok.ZavodniBroj = dokument.ZavodniBroj;
            dok.Datum = dokument.Datum;
            dok.DatumDonosenjaDokumenta = dokument.DatumDonosenjaDokumenta;

        }
        public bool SaveChanges()
        {
            return true;
        }
    }
}
