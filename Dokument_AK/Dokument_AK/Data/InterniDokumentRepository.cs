using AutoMapper;
using Dokument_AK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Data
{
    public class InterniDokumentRepository : IInterniDokumentRepository
    {
        private readonly DokumentContext context;
        private readonly IMapper mapper;


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public InterniDokumentRepository(DokumentContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Cuva sve izmene
        /// </summary>
        /// <returns></returns>

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }


        /// <summary>
        /// Vraca sve dokumente
        /// </summary>
        /// <param name="Izmenjen"></param>
        /// <returns></returns>
        public List<InterniDokumentEnt> GetInterniDokumentEnts(bool Izmenjen = false)
        {
            return context.InterniDokumentEnt.Where(e => (Izmenjen == null)).ToList();
        }

        /// <summary>
        /// Vraca dokument na osnovu IDja
        /// </summary>
        /// <param name="dokumentID"></param>
        /// <returns></returns>

        public InterniDokumentEnt GetInterniDokumentByID(Guid dokumentID)
        {
            return context.InterniDokumentEnt.FirstOrDefault(e => e.DokumentID == dokumentID);
        }


        /// <summary>
        /// Kreira dokument
        /// </summary>
        /// <param name="dokument"></param>
        /// <returns></returns>
        public InterniDokumentConfirmation CreateInterniDokument(InterniDokumentEnt dokument)
        {
            var createdEntity = context.Add(dokument);
            return mapper.Map<InterniDokumentConfirmation>(createdEntity.Entity);
        }


        /// <summary>
        /// Azurira postojeci dokument
        /// </summary>
        /// <param name="dokument"></param>
        public void UpdateInterniDokument(InterniDokumentEnt dokument)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }


        /// <summary>
        /// Brise dokumetn na osnovu IDja
        /// </summary>
        /// <param name="dokumentID"></param>
        public void DeleteInterniDokument(Guid dokumentID)
        {
            var dokument = GetInterniDokumentByID(dokumentID);
            context.Remove(dokument);
        }
    }
}
