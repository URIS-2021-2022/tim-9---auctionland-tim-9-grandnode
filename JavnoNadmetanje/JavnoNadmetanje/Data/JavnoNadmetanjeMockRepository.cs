using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class JavnoNadmetanjeMockRepository : IJavnoNadmetanjeRepository
    {
        public static List<Entities.JavnoNadmetanje> javnoNadmetanjes { get; set; } = new List<Entities.JavnoNadmetanje>();

        public JavnoNadmetanjeMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            javnoNadmetanjes.AddRange(new List<Entities.JavnoNadmetanje>
            {
                new Entities.JavnoNadmetanje
                {
                    JavnoNadmetanjeID = Guid.Parse("208a48a5-371c-4f9d-ac23-18bb176ff8f3"),
                    Datum = DateTime.Parse("2022-2-17"),
                    VremePocetka = DateTime.Parse("2022-2-17T08:00:00"),//godina, mesec, dan, sat, minut, sekunda
                    VremeKraja = DateTime.Parse("2022-2-17T10:00:00"),
                    PocetnaCenaPoHektaru = 5000,
                    Izuzeto = false,
                    TipJavnogNadmetanjaID = Guid.Parse("4246a611-7b2f-429d-a9ba-0e539c81b82f"),
                    IzlicitiranaCena = 7500,
                    PeriodZakupa = 12,
                    BrojUcesnika = 10,
                    VisinaDopuneDepozita = 500,
                    Krug = 1,
                    StatusNadmetanjaID = Guid.Parse("8aaa90c8-56f3-4a76-b07a-f895eded5a84")
                },
                new Entities.JavnoNadmetanje
                {
                    JavnoNadmetanjeID = Guid.Parse("13d6ced2-ab84-4132-bf67-e96037f4813d"),
                    Datum = DateTime.Parse("2022-2-18"),
                    VremePocetka = DateTime.Parse("2022-2-18T08:00:00"),
                    VremeKraja = DateTime.Parse("2022-2-18T10:00:00"),
                    PocetnaCenaPoHektaru = 4000,
                    Izuzeto = false,
                    TipJavnogNadmetanjaID = Guid.Parse("4246a611-7b2f-429d-a9ba-0e539c81b82f"),
                    IzlicitiranaCena = 6000,
                    PeriodZakupa = 12,
                    BrojUcesnika = 10,
                    VisinaDopuneDepozita = 400,
                    Krug = 1,
                    StatusNadmetanjaID = Guid.Parse("8aaa90c8-56f3-4a76-b07a-f895eded5a84")
                }
            });
        }

        public JavnoNadmetanjeConfirmationDto CreateJavnoNadmetanje(Entities.JavnoNadmetanje javnoNadmetanje)
        {
            javnoNadmetanje.JavnoNadmetanjeID = Guid.NewGuid();
            javnoNadmetanjes.Add(javnoNadmetanje);
            Entities.JavnoNadmetanje j = GetJavnoNadmetanjeByID(javnoNadmetanje.JavnoNadmetanjeID);

            return new JavnoNadmetanjeConfirmationDto
            {
                JavnoNadmetanjeID = j.JavnoNadmetanjeID
            };
        }

        public void DeleteJavnoNadmetanje(Guid javnoNadmetanjeID)
        {
            javnoNadmetanjes.Remove(javnoNadmetanjes.FirstOrDefault(j => j.JavnoNadmetanjeID == javnoNadmetanjeID));
        }

        public List<Entities.JavnoNadmetanje> GetJavnaNadmetanja()
        {
            return (from j in javnoNadmetanjes select j).ToList();
        }

        public Entities.JavnoNadmetanje GetJavnoNadmetanjeByID(Guid javnoNadmetanjeID)
        {
            return javnoNadmetanjes.FirstOrDefault(j => j.JavnoNadmetanjeID == javnoNadmetanjeID);
        }

        public JavnoNadmetanjeConfirmationDto UpdateJavnoNadmetanje(Entities.JavnoNadmetanje javnoNadmetanje)
        {
            Entities.JavnoNadmetanje j = GetJavnoNadmetanjeByID(javnoNadmetanje.JavnoNadmetanjeID);

            j.JavnoNadmetanjeID = javnoNadmetanje.JavnoNadmetanjeID;
            j.Datum = javnoNadmetanje.Datum;
            j.VremePocetka = javnoNadmetanje.VremePocetka;
            j.VremeKraja = javnoNadmetanje.VremeKraja;
            j.PocetnaCenaPoHektaru = javnoNadmetanje.PocetnaCenaPoHektaru;
            j.Izuzeto = javnoNadmetanje.Izuzeto;
            j.TipJavnogNadmetanjaID = javnoNadmetanje.TipJavnogNadmetanjaID;
            j.IzlicitiranaCena = javnoNadmetanje.IzlicitiranaCena;
            j.PeriodZakupa = javnoNadmetanje.PeriodZakupa;
            j.BrojUcesnika = javnoNadmetanje.BrojUcesnika;
            j.VisinaDopuneDepozita = javnoNadmetanje.VisinaDopuneDepozita;
            j.Krug = javnoNadmetanje.Krug;
            j.StatusNadmetanjaID = javnoNadmetanje.StatusNadmetanjaID;

            return new JavnoNadmetanjeConfirmationDto
            {
                JavnoNadmetanjeID = j.JavnoNadmetanjeID
            };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
