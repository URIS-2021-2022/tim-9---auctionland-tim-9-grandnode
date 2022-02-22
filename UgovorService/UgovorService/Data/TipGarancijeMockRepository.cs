using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;

namespace UgovorService.Data
{
    public class TipGarancijeMockRepository : ITipGarancijeRepository
    {
        public static List<TipGarancijeEnt> TipGarancijes { get; set; } = new List<TipGarancijeEnt>();
        public TipGarancijeMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            TipGarancijes.AddRange(new List<TipGarancijeEnt>
            {
                new TipGarancijeEnt
                {
                    TipID=Guid.Parse("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                    Tip="Mesecna"

                },
                new TipGarancijeEnt
                {
                    TipID=Guid.Parse("5a36d34e-1620-40c6-ab8c-b96b0be49332"),
                    Tip="Kvartalna"
                }
            });
        }

        public void DeleteGarancije(Guid tipID)
        {
           TipGarancijes.Remove(TipGarancijes.FirstOrDefault(e => e.TipID == tipID));
        }

        public TipGarancijeEnt GetGarancijeByID(Guid tipID)
        {
            return TipGarancijes.FirstOrDefault(e => e.TipID == tipID);
        }

        public List<TipGarancijeEnt> GetGarancijes(string Tip = null)
        {
            return (from e in TipGarancijes
                    where string.IsNullOrEmpty(Tip) || e.Tip == Tip
                    select e).ToList();
        }

        public void UpdateGarancije(TipGarancijeEnt garancija)
        {
            TipGarancijeEnt ugo = GetGarancijeByID(garancija.TipID);

            ugo.TipID = garancija.TipID;
            ugo.Tip = garancija.Tip;

        }

        public TipGarancijeConfirmation CreateGarancije(TipGarancijeEnt garancija)
        {
            garancija.TipID = Guid.NewGuid();
            TipGarancijes.Add(garancija);
            TipGarancijeEnt ugo = GetGarancijeByID(garancija.TipID);

            return new TipGarancijeConfirmation
            {
                TipID = ugo.TipID,
                Tip=ugo.Tip
            };
        }
        public bool SaveChanges()
        {
            return true;
        }
    }
}
