using AutoMapper;
using Dokument_AK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Data
{
    public class StatusDokumentaRepository : IStatusDokumentaRepository
    {
        private readonly DokumentContext context;
        private readonly IMapper mapper;


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public StatusDokumentaRepository(DokumentContext context, IMapper mapper)
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
        /// Vraca sve statuse dokumenata
        /// </summary>
        /// <param name="Usvojen"></param>
        /// <returns></returns>

        public List<StatusDokumentaEnt> GetStatusDokumentaEnts(bool Usvojen = false)
        {
            return context.StatusDokumentaEnt.Where(e => (Usvojen == false)).ToList();
        }


        /// <summary>
        /// Vraca status dokumenta na osnovu IDja
        /// </summary>
        /// <param name="statusDokID"></param>
        /// <returns></returns>

        public StatusDokumentaEnt GetStatusDokumentaByID(Guid statusDokID)
        {
            return context.StatusDokumentaEnt.FirstOrDefault(e => e.StatusDokID == statusDokID);
        }


        /// <summary>
        /// Krerira novi status dokumenta
        /// </summary>
        /// <param name="dokument"></param>
        /// <returns></returns>
        public StatusDokumentaConfirmation CreateStatusDokument(StatusDokumentaEnt dokument)
        {
            var createdEntity = context.Add(dokument);
            return mapper.Map<StatusDokumentaConfirmation>(createdEntity.Entity);
        }

        /// <summary>
        /// Azurira postojeci status dokumenta
        /// </summary>
        /// <param name="dokument"></param>

        public void UpdateStatusDokumenta(StatusDokumentaEnt dokument)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        /// <summary>
        /// Brise status dokumenta na osnovu IDja
        /// </summary>
        /// <param name="statusDokID"></param>

        public void DeleteStatusDokumenta(Guid statusDokID)
        {
            var dokument = GetStatusDokumentaByID(statusDokID);
            context.Remove(dokument);
        }
    }
}
