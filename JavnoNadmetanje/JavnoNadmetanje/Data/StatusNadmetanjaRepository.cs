using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class StatusNadmetanjaRepository : IStatusNadmetanjaRepository
    {
        private readonly JavnoNadmetanjeContext context;
        private readonly IMapper mapper;

        public StatusNadmetanjaRepository(JavnoNadmetanjeContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public StatusNadmetanjaConfirmationDto CreateStatusNadmetanja(StatusNadmetanja statusNadmetanja)
        {
            statusNadmetanja.StatusNadmetanjaID = Guid.NewGuid();
            var noviEntitet = context.StatusiNadmetanja.Add(statusNadmetanja);
            //context.SaveChanges();

            //return mapper.Map<StatusNadmetanjaConfirmationDto>(statusNadmetanja);
            return mapper.Map<StatusNadmetanjaConfirmationDto>(noviEntitet.Entity);
        }

        public void DeleteStatusNadmetanja(Guid statusNadmetanjaID)
        {
            StatusNadmetanja status = GetStatusNadmetanjaByID(statusNadmetanjaID);
            context.StatusiNadmetanja.Remove(status);
        }

        public List<StatusNadmetanja> GetStatusiNadmetanja()
        {
            return context.StatusiNadmetanja.ToList();
        }

        public StatusNadmetanja GetStatusNadmetanjaByID(Guid statusNadmetanjaID)
        {
            return context.StatusiNadmetanja.FirstOrDefault(s => s.StatusNadmetanjaID == statusNadmetanjaID);
        }

        public StatusNadmetanjaConfirmationDto UpdateStatusNadmetanja(StatusNadmetanja statusNadmetanja)
        {
            throw new NotImplementedException();
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene ce biti perzistirane
        }
    }
}
