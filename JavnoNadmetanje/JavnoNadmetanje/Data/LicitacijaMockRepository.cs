using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class LicitacijaMockRepository : ILicitacijaRepository
    {
        public static List<Licitacija> licitacijas { get; set; } = new List<Licitacija>();

        public LicitacijaMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            licitacijas.AddRange(new List<Licitacija>
            {
                new Licitacija
                {
                    LicitacijaID = Guid.Parse("a215e4cb-a427-40cf-88b2-8488d140a939"),
                    Broj = 1,
                    Godina = 2022,
                    Datum = DateTime.Parse("2022-2-17"),
                    Ogranicenja = 1,
                    KorakCene = 100,
                    ListaDokumentacijeFizickaLica = new List<string>(){ "dok1_fl", "dok2_fl"},
                    ListaDokumentacijePravnaLica = new List<string>(){ "dok1_pl", "dok1_pl"},
                    JavnoNadmetanjeID = Guid.Parse("208a48a5-371c-4f9d-ac23-18bb176ff8f3"),
                    RokPrijava = DateTime.Parse("2022-2-15")
                },
                new Licitacija
                {
                    LicitacijaID = Guid.Parse("1de13266-85e8-4120-8b1f-daacc32c5811"),
                    Broj = 2,
                    Godina = 2022,
                    Datum = DateTime.Parse("2022-2-18"),
                    Ogranicenja = 1,
                    KorakCene = 200,
                    ListaDokumentacijeFizickaLica = new List<string>(){ "dok1_fl", "dok2_fl"},
                    ListaDokumentacijePravnaLica = new List<string>(){ "dok1_pl", "dok1_pl"},
                    JavnoNadmetanjeID = Guid.Parse("208a48a5-371c-4f9d-ac23-18bb176ff8f3"),
                    RokPrijava = DateTime.Parse("2022-2-16")
                }
            });
        }

        public LicitacijaConfirmationDto CreateLicitacija(Licitacija licitacija)
        {
            licitacija.LicitacijaID = Guid.NewGuid();
            licitacijas.Add(licitacija);
            Licitacija l = GetLicitacijaByID(licitacija.LicitacijaID);

            return new LicitacijaConfirmationDto
            {
                LicitacijaID = l.LicitacijaID
            };
        }

        public void DeleteLicitacija(Guid licitacijaID)
        {
            licitacijas.Remove(licitacijas.FirstOrDefault(l => l.LicitacijaID == licitacijaID));
        }

        public List<Licitacija> GetLicitacije()
        {
            return (from l in licitacijas select l).ToList();
        }

        public Licitacija GetLicitacijaByID(Guid licitacijaID)
        {
            return licitacijas.FirstOrDefault(l => l.LicitacijaID == licitacijaID);
        }

        public LicitacijaConfirmationDto UpdateLicitacija(Licitacija licitacija)
        {
            Licitacija l = GetLicitacijaByID(licitacija.LicitacijaID);

            l.LicitacijaID = licitacija.LicitacijaID;
            l.Broj = licitacija.Broj;
            l.Godina = licitacija.Godina;
            l.Datum = licitacija.Datum;
            l.Ogranicenja = licitacija.Ogranicenja;
            l.KorakCene = licitacija.KorakCene;
            l.ListaDokumentacijeFizickaLica = licitacija.ListaDokumentacijeFizickaLica;
            l.ListaDokumentacijePravnaLica = licitacija.ListaDokumentacijePravnaLica;
            l.JavnoNadmetanjeID = licitacija.JavnoNadmetanjeID;
            l.RokPrijava = licitacija.RokPrijava;

            return new LicitacijaConfirmationDto
            {
                LicitacijaID = l.LicitacijaID
            };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
