using AutoMapper;
using Dokument_AK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Data
{
    public class EksterniDokumentRepository : IEksterniDokumentRepository
    {

        private readonly DokumentContext context;
        private readonly IMapper mapper;

        public EksterniDokumentRepository(DokumentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        /// <summary>
        /// Metoda koja vraca sve dokumente
        /// </summary>
        /// <param name="Izmenjen">Filter parametar izmenjen</param>
        /// <returns></returns>
        public List<EksterniDokumentEnt> GetEksterniDokumentEnts(bool Izmenjen = false)
        {
            return context.EksterniDokumentEnt.Where(e => (Izmenjen == false)).ToList();
        }

        /// <summary>
        /// Metoda koja vraca dokument na osnovu IDja
        /// </summary>
        /// <param name="dokumentID">ID dokumenta</param>
        /// <returns></returns>

        public EksterniDokumentEnt GetEskterniDokumentByID(Guid dokumentID)
        {
            return context.EksterniDokumentEnt.FirstOrDefault(e => e.DokumentID == dokumentID);
        }


        /// <summary>
        /// Metoda koja kreira dokument
        /// </summary>
        /// <param name="dokumentID">Potrebno je proslediti telo dokumenta</param>
        /// <returns></returns>
        public EksterniDokumentConfirmation CreateEksterniDokument(EksterniDokumentEnt dokumentID)
        {
            var createdEntity = context.Add(dokumentID);
            return mapper.Map<EksterniDokumentConfirmation>(createdEntity.Entity);
        }

        /// <summary>
        /// Metoda koja azurira postojeci dokument
        /// </summary>
        /// <param name="dokumentID">Vrsi se na osnovu IDja</param>
        public void UpdateEksterniDokument(EksterniDokumentEnt dokumentID)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }


        /// <summary>
        /// Brisanje dokumenta
        /// </summary>
        /// <param name="dokumentID">Potrebno je unedi ID dokumenta</param>
        public void DeleteEksterniDokument(Guid dokumentID)
        {
            var dokument = GetEskterniDokumentByID(dokumentID);
            context.Remove(dokument);
        }
    }
}
