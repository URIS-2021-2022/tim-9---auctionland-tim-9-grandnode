using AutoMapper;
using Dokument_AK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Data
{
    public class DokumentRepository : IDokumentRepository
    {
        private readonly DokumentContext context;
        private readonly IMapper mapper;

        public DokumentRepository(DokumentContext context, IMapper mapper)
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
        /// <param name="ZavodniBroj">Filter polje zavodni broj</param>
        /// <returns></returns>
        public List<DokumentEnt> GetDokuments(string ZavodniBroj = null)
        {
            return context.DokumentEnt.Where(e => (ZavodniBroj == null)).ToList();
        }


        /// <summary>
        /// Vraca dokument na osnovu IDja
        /// </summary>
        /// <param name="dokumentID">ID dokumenta</param>
        /// <returns></returns>
        public DokumentEnt GetDokumentByID(Guid dokumentID)
        {
            return context.DokumentEnt.FirstOrDefault(e => e.DokumentID == dokumentID);
        }

        /// <summary>
        /// Metoda koja kreira dokument
        /// </summary>
        /// <param name="dokument">Potrebno je unedi telo dokumenta</param>
        /// <returns></returns>
        public DokumentConfirmation CreateDokument(DokumentEnt dokument)
        {
            var createdEntity = context.Add(dokument);
            return mapper.Map<DokumentConfirmation>(createdEntity.Entity);
        }

        /// <summary>
        /// Azurira postojeci dokument
        /// </summary>
        /// <param name="dokument">Potrebno je unesti dokument</param>

        public void UpdateDokument(DokumentEnt dokument)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }


        /// <summary>
        /// Brise dokument
        /// </summary>
        /// <param name="dokumentID">ID dokumenta</param>
        public void DeleteDokument(Guid dokumentID)
        {
            var dokument= GetDokumentByID(dokumentID);
            context.Remove(dokument);
        }
    }
}
