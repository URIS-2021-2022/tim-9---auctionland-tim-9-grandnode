using Kupac_SK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Data
{
    public class PravnoLiceRepository : IPravnoLiceRepository
    {
        public static List<PravnoLice> Lica { get; set; } = new List<PravnoLice>();

        public PravnoLiceRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Lica.AddRange(new List<PravnoLice>
            {
                new PravnoLice
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
                    BrojRacuna ="170000000082",
                    Naziv = "doo x",
                    MatBr = "12345678",
                    Faks = "741258"
                }

            });
        }
        public PravnoLice CreatePravnoLice(PravnoLice pravnoLice)
        {
            throw new NotImplementedException();
        }

        public void DeletePravnoLice(Guid pravnoLice)
        {
            Lica.Remove(Lica.FirstOrDefault(e => e.KupacID == pravnoLice));
        }

        public List<PravnoLice> getPravnaLica()
        {
            return Lica.ToList();
        }

        public PravnoLice GetPravnoLiceById(Guid plID)
        {
            return Lica.FirstOrDefault(e => e.KupacID == plID);
        }

        public void UpdatePravnoLice(PravnoLice pravnoLice)
        {
           //
        }
    }
}
