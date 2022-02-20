using Dokument_AK.Entities;
using Dokument_AK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Data
{
    public class EksterniDokumentMockRepository : IEksterniDokumentRepository
    {
        public static List<EksterniDokumentEnt> EksterniDokumentEnts { get; set; } = new List<EksterniDokumentEnt>();

        public EksterniDokumentMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            EksterniDokumentEnts.AddRange(new List<EksterniDokumentEnt>
            {
                new EksterniDokumentEnt
                {
                    DokumentID = Guid.Parse("2f530032-429e-4be7-b202-d800876d393d"),
                    Izmenjen=true

                },
                new EksterniDokumentEnt
                {
                    DokumentID= Guid.Parse("4ff5c5a0-93d4-443d-bf2c-dc9cf9fa4296"),
                     Izmenjen=false
                }
            }); ;
        }
        public EksterniDokumentEnt GetEskterniDokumentByID(Guid dokumentID)
        {
            return EksterniDokumentEnts.FirstOrDefault(D => D.DokumentID == dokumentID);
        }

        public List<EksterniDokumentEnt> GetEksterniDokumentEnts(bool Izmenjen = false)
        {
            return (from e in EksterniDokumentEnts
                    where Izmenjen == false || Izmenjen == true || e.Izmenjen == Izmenjen
                    select e).ToList();
        }

        public EksterniDokumentConfirmation CreateEksterniDokument(EksterniDokumentEnt dokument)
        {
            dokument.DokumentID = Guid.NewGuid();
            EksterniDokumentEnts.Add(dokument);
            var d = GetEskterniDokumentByID(dokument.DokumentID);

            return new EksterniDokumentConfirmation
            {
                DokumentID = d.DokumentID,
                Izmenjen = d.Izmenjen
            };
        }

        public void DeleteEksterniDokument(Guid dokumentID)
        {
            EksterniDokumentEnts.Remove(EksterniDokumentEnts.FirstOrDefault(e => e.DokumentID == dokumentID));
        }

        public void UpdateEksterniDokument(EksterniDokumentEnt dokument)
        {
            EksterniDokumentEnt dok = GetEskterniDokumentByID(dokument.DokumentID);

            dok.DokumentID = dokument.DokumentID;
            dok.Izmenjen = dokument.Izmenjen;


        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
