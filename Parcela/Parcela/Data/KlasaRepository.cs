using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class KlasaRepository : IKlasaRepository
    {
        public static List<Klasa> KlasaLista { get; set; } = new List<Klasa>();
        public KlasaRepository()
        {
            FillData();
        }
        private void FillData()
        {
            KlasaLista.AddRange(new List<Klasa>
            {
                new Klasa
                {
                    KlasaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    NazivKlase = "Prva klasa"
                }
            });
        }
        public Klasa GetKlasaById(Guid klasaId)
        {
            return KlasaLista.FirstOrDefault(k => k.KlasaID == klasaId);
        }

        public List<Klasa> GetKlasaList()
        {
            return KlasaLista;
        }
    }
}
