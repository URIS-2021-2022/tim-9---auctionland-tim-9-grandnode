using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;

namespace UgovorService.Data
{
    public class UgovorMockRepository : IUgovorRepository
    {
        public static List<UgovorEnt> Ugovors { get; set; } = new List<UgovorEnt>();
        public UgovorMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Ugovors.AddRange(new List<UgovorEnt>
            {
                new UgovorEnt
                {
                    UgovorID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    TipID=Guid.Parse("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                    DokumentID=Guid.Parse("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                    ZavodniBr = "471/RS",
                    JavnoNadmetanjeID= Guid.Parse("bf33a825-ad63-4e04-a812-74ffbebdadbb"),
                    DatumZavo=DateTime.Parse("2020-11-15T09:00:00"),
                    KupacID=Guid.Parse("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                    Rok=DateTime.Parse("2023-11-15T09:00:00"),
                    Mesto="Novi Sad",
                    DatumPot=DateTime.Parse("2020-12-15T09:00:00")

                },
                new UgovorEnt
                {
                    UgovorID = Guid.Parse("6f4967b3-beb3-4acf-95aa-488b16f8fc9a"),
                    TipID=Guid.Parse("5a36d34e-1620-40c6-ab8c-b96b0be49332"),
                    DokumentID=Guid.Parse("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                    ZavodniBr = "471/RS",
                    JavnoNadmetanjeID= Guid.Parse("5a36d34e-1620-40c6-ab8c-b96b0be49332"),
                    DatumZavo=DateTime.Parse("2019-11-15T09:00:00"),
                    KupacID=Guid.Parse("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                    Rok=DateTime.Parse("2022-11-15T09:00:00"),
                    Mesto="Beograd",
                    DatumPot=DateTime.Parse("2019-12-15T09:00:00")
                }
            });
        }




        public List<UgovorEnt> GetUgovors(string ZavodniBr = null)
        {
            return (from e in Ugovors
                    where string.IsNullOrEmpty(ZavodniBr) || e.ZavodniBr == ZavodniBr
                    select e).ToList();
        }

        public UgovorEnt GetUgovorByID(Guid ugovorID)
        {
            return Ugovors.FirstOrDefault(e => e.UgovorID== ugovorID);
        }



        public UgovorConfirmation CreateUgovor(UgovorEnt ugovor)
        {
            ugovor.UgovorID = Guid.NewGuid();
            Ugovors.Add(ugovor);
            UgovorEnt ugo = GetUgovorByID(ugovor.UgovorID);

            return new UgovorConfirmation
            {
                UgovorID = ugo.UgovorID,
                TipID = ugo.TipID,
                DokumentID=ugo.DokumentID,
                ZavodniBr = ugo.ZavodniBr,
                JavnoNadmetanjeID = ugo.JavnoNadmetanjeID,
                DatumZavo = ugo.DatumZavo,
                KupacID = ugo.KupacID,
                Rok = ugo.Rok,
                Mesto = ugo.Mesto,
                DatumPot = ugo.DatumPot
            };
        }


        public void UpdateUgovor(UgovorEnt ugovor)
        {
            UgovorEnt ugo = GetUgovorByID(ugovor.UgovorID);

            ugo.UgovorID = ugovor.UgovorID;
            ugo.TipID = ugovor.TipID;
            ugo.DokumentID = ugovor.DokumentID;
            ugo.ZavodniBr = ugovor.ZavodniBr;
            ugo.JavnoNadmetanjeID = ugovor.JavnoNadmetanjeID;
            ugo.DatumZavo = ugovor.DatumZavo;
            ugo.KupacID = ugovor.KupacID;
            ugo.Rok = ugovor.Rok;
            ugo.Mesto = ugovor.Mesto;
            ugo.DatumPot = ugovor.DatumPot;


        }

        public void DeleteUgovor(Guid ugovorID)
        {
            Ugovors.Remove(Ugovors.FirstOrDefault(e => e.UgovorID == ugovorID));
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
