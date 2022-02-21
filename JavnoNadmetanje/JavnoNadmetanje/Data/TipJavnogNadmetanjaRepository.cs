using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public class TipJavnogNadmetanjaRepository : ITipJavnogNadmetanjaRepository
    {
        private readonly JavnoNadmetanjeContext context;
        private readonly IMapper mapper;

        public TipJavnogNadmetanjaRepository(JavnoNadmetanjeContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public TipJavnogNadmetanjaConfirmationDto CreateTipJavnogNadmetanja(TipJavnogNadmetanja tipJavnogNadmetanja)
        {
            tipJavnogNadmetanja.TipJavnogNadmetanjaID = Guid.NewGuid();
            var noviEntitet = context.TipoviJavnihNadmetanja.Add(tipJavnogNadmetanja);
            //context.SaveChanges();

            //return mapper.Map<TipJavnogNadmetanjaConfirmationDto>(tipJavnogNadmetanja);
            return mapper.Map<TipJavnogNadmetanjaConfirmationDto>(noviEntitet.Entity);
        }

        public void DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID)
        {
            TipJavnogNadmetanja tip = GetTipJavnogNadmetanjaByID(tipJavnogNadmetanjaID);
            context.TipoviJavnihNadmetanja.Remove(tip);
        }

        public List<TipJavnogNadmetanja> GetTipoviJavnogNadmetanja()
        {
            return context.TipoviJavnihNadmetanja.ToList();
        }

        public TipJavnogNadmetanja GetTipJavnogNadmetanjaByID(Guid tipJavnogNadmetanjaID)
        {
            return context.TipoviJavnihNadmetanja.FirstOrDefault(t => t.TipJavnogNadmetanjaID == tipJavnogNadmetanjaID);
        }

        public TipJavnogNadmetanjaConfirmationDto UpdateTipJavnogNadmetanja(TipJavnogNadmetanja tipJavnogNadmetanja)
        {
            throw new NotImplementedException();
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene ce biti perzistirane
        }
    }
}
