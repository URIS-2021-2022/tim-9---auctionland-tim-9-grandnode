using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class JavnoNadmetanjeRepository : IJavnoNadmetanjeRepository
    {
        private readonly JavnoNadmetanjeContext context;
        private readonly IMapper mapper;

        public JavnoNadmetanjeRepository(JavnoNadmetanjeContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public JavnoNadmetanjeConfirmationDto CreateJavnoNadmetanje(Entities.JavnoNadmetanje javnoNadmetanje)
        {
            javnoNadmetanje.JavnoNadmetanjeID = Guid.NewGuid();
            var noviEntitet = context.JavnaNadmetanja.Add(javnoNadmetanje);
            //context.SaveChanges();

            //return mapper.Map<JavnoNadmetanjeConfirmationDto>(javnoNadmetanje);
            return mapper.Map<JavnoNadmetanjeConfirmationDto>(noviEntitet.Entity);
        }

        public void DeleteJavnoNadmetanje(Guid javnoNadmetanjeID)
        {
            Entities.JavnoNadmetanje nadmetanje = GetJavnoNadmetanjeByID(javnoNadmetanjeID);
            context.JavnaNadmetanja.Remove(nadmetanje);
        }

        public List<Entities.JavnoNadmetanje> GetJavnaNadmetanja()
        {
            return context.JavnaNadmetanja.ToList();
        }

        public Entities.JavnoNadmetanje GetJavnoNadmetanjeByID(Guid javnoNadmetanjeID)
        {
            return context.JavnaNadmetanja.FirstOrDefault(j => j.JavnoNadmetanjeID == javnoNadmetanjeID);
        }

        public JavnoNadmetanjeConfirmationDto UpdateJavnoNadmetanje(Entities.JavnoNadmetanje javnoNadmetanje)
        {
            throw new NotImplementedException();
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene ce biti perzistirane
        }
    }
}
