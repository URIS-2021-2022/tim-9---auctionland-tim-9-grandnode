using Kupac_SK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Data
{
    public class FizickoLiceRepository : IFizickoLiceRepository
    {
        public static List<FizickoLice> Lica { get; set; } = new List<FizickoLice>();

        public FizickoLiceRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Lica.AddRange(new List<FizickoLice>
            {
                new FizickoLice
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
                    BrojRacuna ="170000000082",
                    JMBG = "160999979894",
                    Ime = "Sara",
                    Prezime = "Kijanovic"
                }

            });
        }
        public FizickoLice CreateFizickoLice(FizickoLice fizickoLice)
        {
            throw new NotImplementedException();
        }

        public void DeleteFizickoLice(Guid flID)
        {
            Lica.Remove(Lica.FirstOrDefault(e => e.KupacID == flID));
        }

        public List<FizickoLice> GetFizickaLica()
        {
            return Lica.ToList();
        }

        public FizickoLice GetFizickoLiceById(Guid flID)
        {
            return Lica.FirstOrDefault(e => e.KupacID == flID);
        }

        public void UpdateFizickoLice(FizickoLice fizickoLice)
        {
            //
        }
    }
}
