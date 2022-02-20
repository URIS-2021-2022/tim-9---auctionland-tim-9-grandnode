using Kupac_SK.Entities;
using Kupac_SK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Data
{
    public class KupacRepository : IKupacRepository
    {
        public static List<KupacModel> Kupci { get; set; } = new List<KupacModel>();

        public KupacRepository()
        {
            FillData();
        }


        private void FillData()
        {
            Kupci.AddRange(new List<KupacModel>
            {
                new KupacModel
                {
                    KupacID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    FizPravno = true,
                    OstvarenaPovrsina = "15000",
                    Zabrana = false,
                    PocetakZabrane = DateTime.Parse("1900-01-01T09:00:00"),
                    DuzinaZabrane = "0",
                    PrestanakZabrane = DateTime.Parse("1900-01-01T09:00:00"),
                    OvlascenoLiceID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    PrioritetID = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    BrTel1 = "064111558",
                    BrTel2 = "225447",
                    Email = "imejl@gmail.com",
                    AdresaID = "bulevar 13",
                     UplataID ="yyyyyyyyyyyyyyy",
                    BrojRacuna ="170000000082"
                },

                new KupacModel
                {
                    KupacID = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                    FizPravno = false,
                    OstvarenaPovrsina = "15000",
                    Zabrana = false,
                    PocetakZabrane = DateTime.Parse("1900-01-01T09:00:00"),
                    DuzinaZabrane = "0",
                    PrestanakZabrane = DateTime.Parse("1900-01-01T09:00:00"),
                    OvlascenoLiceID = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    PrioritetID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    BrTel1 = "064111558",
                    BrTel2 = "225447",
                    Email = "imejl@gmail.com",
                    AdresaID = "bulevar 13",
                    UplataID ="xxxx",
                    BrojRacuna ="170000000082"

                }


            }); ;
        }
        public KupacConfirmation CreateKupac(KupacModel kupacModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteKupac(Guid kupacId)
        {
            Kupci.Remove(Kupci.FirstOrDefault(e => e.KupacID == kupacId));
        }

        public KupacModel GetKupacById(Guid kupacId)
        {
         
            return Kupci.FirstOrDefault(e => e.KupacID == kupacId);
        }

        public List<KupacModel> GetKupci()
        {
            return Kupci.ToList();
        }

        public void UpdateKupac(KupacModel kupacModel)
        {
            throw new NotImplementedException();
        }
    }
}
