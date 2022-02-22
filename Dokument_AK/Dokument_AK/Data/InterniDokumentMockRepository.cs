using Dokument_AK.Entities;
using Dokument_AK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Data
{
    public class InterniDokumentMockRepository : IInterniDokumentRepository
    {
        public static List<InterniDokumentEnt> InterniDokumentEnts { get; set; } = new List<InterniDokumentEnt>();

        public InterniDokumentMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            InterniDokumentEnts.AddRange(new List<InterniDokumentEnt>
            {
                new InterniDokumentEnt
                {
                    DokumentID = Guid.Parse("2f530032-429e-4be7-b202-d800876d393d"),
                    Izmenjen=true

                },
                new InterniDokumentEnt
                {
                    DokumentID= Guid.Parse("4ff5c5a0-93d4-443d-bf2c-dc9cf9fa4296"),
                     Izmenjen=false
                }
            }); 
        }
        public InterniDokumentEnt GetInterniDokumentByID(Guid dokumentID)
        {
            return InterniDokumentEnts.FirstOrDefault(D => D.DokumentID == dokumentID);
        }

        public List<InterniDokumentEnt> GetInterniDokumentEnts(bool Izmenjen = false)
        {
            return (from e in InterniDokumentEnts
                    where Izmenjen == false || Izmenjen != false || e.Izmenjen == Izmenjen
                    select e).ToList();
        }

        public InterniDokumentConfirmation CreateInterniDokument(InterniDokumentEnt dokument)
        {
            dokument.DokumentID = Guid.NewGuid();
            InterniDokumentEnts.Add(dokument);
            var d = GetInterniDokumentByID(dokument.DokumentID);

            return new InterniDokumentConfirmation
            {
                DokumentID=d.DokumentID,
                Izmenjen=d.Izmenjen
            };
        }

        public void DeleteInterniDokument(Guid dokumentID)
        {
            InterniDokumentEnts.Remove(InterniDokumentEnts.FirstOrDefault(e => e.DokumentID == dokumentID));
        }

        public void UpdateInterniDokument(InterniDokumentEnt dokument)
        {
            InterniDokumentEnt dok = GetInterniDokumentByID(dokument.DokumentID);

            dok.DokumentID = dokument.DokumentID;
            dok.Izmenjen = dokument.Izmenjen;


        }
        public bool SaveChanges()
        {
            return true;
        }
    }
}
