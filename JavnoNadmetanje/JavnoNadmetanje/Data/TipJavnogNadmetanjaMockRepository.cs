using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class TipJavnogNadmetanjaMockRepository : ITipJavnogNadmetanjaRepository
    {
        public static List<TipJavnogNadmetanja> tipJavnogNadmetanjas { get; set; } = new List<TipJavnogNadmetanja>();

        public TipJavnogNadmetanjaMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            tipJavnogNadmetanjas.AddRange(new List<TipJavnogNadmetanja>
            {
                new TipJavnogNadmetanja
                {
                    TipJavnogNadmetanjaID = Guid.Parse("4246a611-7b2f-429d-a9ba-0e539c81b82f"),
                    NazivTipaJavnogNadmetanja = "Javna licitacija"
                },
                new TipJavnogNadmetanja
                {
                    TipJavnogNadmetanjaID = Guid.Parse("99b6d6ec-4358-4898-936b-31b31d236324"),
                    NazivTipaJavnogNadmetanja = "Otvaranje zatvorenih ponuda"
                }
            });
        }

        public TipJavnogNadmetanjaConfirmationDto CreateTipJavnogNadmetanja(TipJavnogNadmetanja tipJavnogNadmetanja)
        {
            tipJavnogNadmetanja.TipJavnogNadmetanjaID = Guid.NewGuid();
            tipJavnogNadmetanjas.Add(tipJavnogNadmetanja);
            TipJavnogNadmetanja t = GetTipJavnogNadmetanjaByID(tipJavnogNadmetanja.TipJavnogNadmetanjaID);

            return new TipJavnogNadmetanjaConfirmationDto
            {
                TipJavnogNadmetanjaID = t.TipJavnogNadmetanjaID
            };
        }

        public void DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            tipJavnogNadmetanjas.Remove(tipJavnogNadmetanjas.FirstOrDefault(t => t.TipJavnogNadmetanjaID == tipJavnogNadmetanjaID));
        }

        public List<TipJavnogNadmetanja> GetTipoviJavnogNadmetanja()
        {
            return (from t in tipJavnogNadmetanjas select t).ToList();
        }

        public TipJavnogNadmetanja GetTipJavnogNadmetanjaByID(Guid tipJavnogNadmetanjaID)
        {
            return tipJavnogNadmetanjas.FirstOrDefault(t => t.TipJavnogNadmetanjaID == tipJavnogNadmetanjaID);
        }

        public TipJavnogNadmetanjaConfirmationDto UpdateTipJavnogNadmetanja(TipJavnogNadmetanja tipJavnogNadmetanja)
        {
            TipJavnogNadmetanja t = GetTipJavnogNadmetanjaByID(tipJavnogNadmetanja.TipJavnogNadmetanjaID);

            t.TipJavnogNadmetanjaID = tipJavnogNadmetanja.TipJavnogNadmetanjaID;
            t.NazivTipaJavnogNadmetanja = tipJavnogNadmetanja.NazivTipaJavnogNadmetanja;

            return new TipJavnogNadmetanjaConfirmationDto
            {
                TipJavnogNadmetanjaID = t.TipJavnogNadmetanjaID
            };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
