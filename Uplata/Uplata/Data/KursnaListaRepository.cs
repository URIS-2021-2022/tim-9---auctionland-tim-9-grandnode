using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;
using Uplata.Models;

namespace Uplata.Data
{
    public class KursnaListaRepository : IKursnaListaRepository
    {
        private readonly UplataContext context;
        private readonly IMapper mapper;

        public KursnaListaRepository(UplataContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public KursnaListaConfirmationDto CreateKursnaLista(KursnaLista kursnaLista)
        {
            kursnaLista.KursnaListaID = Guid.NewGuid();
            var noviEntitet = context.KursneListe.Add(kursnaLista);
            //context.SaveChanges();

            //return mapper.Map<KursnaListaConfirmationDto>(kursnaLista);
            return mapper.Map<KursnaListaConfirmationDto>(noviEntitet.Entity);
        }

        public void DeleteKursnaLista(Guid kursnaListaID)
        {
            KursnaLista lista = GetKursnaListaByID(kursnaListaID);
            context.KursneListe.Remove(lista);
        }

        public List<KursnaLista> GetKursneListe()
        {
            return context.KursneListe.ToList();
        }

        public KursnaLista GetKursnaListaByID(Guid kursnaListaID)
        {
            return context.KursneListe.FirstOrDefault(k => k.KursnaListaID == kursnaListaID);
        }

        public KursnaListaConfirmationDto UpdateKursnaLista(KursnaLista kursnaLista)
        {
            throw new NotImplementedException();
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene ce biti perzistirane
        }
    }
}
