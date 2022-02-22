using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class LicitacijaRepository : ILicitacijaRepository
    {
        private readonly JavnoNadmetanjeContext context;
        private readonly IMapper mapper;

        public LicitacijaRepository(JavnoNadmetanjeContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public LicitacijaConfirmationDto CreateLicitacija(Licitacija licitacija)
        {
            licitacija.LicitacijaID = Guid.NewGuid();
            var noviEntitet = context.Licitacije.Add(licitacija);

            //return mapper.Map<LicitacijaConfirmationDto>(licitacija);
            return mapper.Map<LicitacijaConfirmationDto>(noviEntitet.Entity);
        }

        public void DeleteLicitacija(Guid licitacijaID)
        {
            Licitacija licitacija = GetLicitacijaByID(licitacijaID);
            context.Licitacije.Remove(licitacija);
        }

        public List<Licitacija> GetLicitacije()
        {
            return context.Licitacije.ToList();
        }

        public Licitacija GetLicitacijaByID(Guid licitacijaID)
        {
            return context.Licitacije.FirstOrDefault(l => l.LicitacijaID == licitacijaID);
        }

        public LicitacijaConfirmationDto UpdateLicitacija(Licitacija licitacija)
        {
            throw new NotImplementedException();
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene ce biti perzistirane
        }
    }
}
