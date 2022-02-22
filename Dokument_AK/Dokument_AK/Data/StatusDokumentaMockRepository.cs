using Dokument_AK.Entities;
using Dokument_AK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Data
{
    public class StatusDokumentaMockRepository : IStatusDokumentaRepository
    {
        public static List<StatusDokumentaEnt> StatusDokumentaEnts { get; set; } = new List<StatusDokumentaEnt>();

        public StatusDokumentaMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            StatusDokumentaEnts.AddRange(new List<StatusDokumentaEnt>
            {
                new StatusDokumentaEnt
                {
                    StatusDokID = Guid.Parse("2f530032-429e-4be7-b202-d800876d393d"),
                    Usvojen = true,
                    Odbijen=false,
                    Otvoren=false

                },
                new StatusDokumentaEnt
                {
                    StatusDokID = Guid.Parse("4ff5c5a0-93d4-443d-bf2c-dc9cf9fa4296"),
                    Usvojen = false,
                    Odbijen=false,
                    Otvoren=true
                }
            });
        }
        public StatusDokumentaEnt GetStatusDokumentaByID(Guid statusDokID)
        {
            return StatusDokumentaEnts.FirstOrDefault(D => D.StatusDokID == statusDokID);
        }

        public List<StatusDokumentaEnt> GetStatusDokumentaEnts(bool Usvojen = false)
        {
            return (from e in StatusDokumentaEnts
                    where Usvojen == false || Usvojen!=true || e.Usvojen == Usvojen
                    select e).ToList();
        }

        public StatusDokumentaConfirmation CreateStatusDokument(StatusDokumentaEnt dokument)
        {
            dokument.StatusDokID = Guid.NewGuid();
            StatusDokumentaEnts.Add(dokument);
            var d = GetStatusDokumentaByID((Guid)dokument.StatusDokID);

            return new StatusDokumentaConfirmation
            {
                StatusDokID = (Guid)d.StatusDokID,
                Usvojen=d.Usvojen,
                Odbijen=d.Odbijen,
                Otvoren=d.Otvoren
            };
        }

        public void DeleteStatusDokumenta(Guid statusDokID)
        {
            StatusDokumentaEnts.Remove(StatusDokumentaEnts.FirstOrDefault(e => e.StatusDokID == statusDokID));
        }

        public void UpdateStatusDokumenta(StatusDokumentaEnt dokument)
        {
            StatusDokumentaEnt dok = GetStatusDokumentaByID((Guid)dokument.StatusDokID);

            dok.StatusDokID = dokument.StatusDokID;
            dok.Usvojen = dokument.Usvojen;
            dok.Odbijen = dokument.Odbijen;
            dok.Otvoren = dokument.Otvoren;

        }

        public bool SaveChanges()
        {
            return true;
        }

    }
}
