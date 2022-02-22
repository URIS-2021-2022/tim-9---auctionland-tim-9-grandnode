using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;
using Uplata.Models;

namespace Uplata.Data
{
    public class UplataRepository : IUplataRepository
    {
        private readonly UplataContext context;
        private readonly IMapper mapper;

        public UplataRepository(UplataContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public UplataConfirmationDto CreateUplata(Entities.Uplata uplata)
        {
            uplata.UplataID = Guid.NewGuid();
            var noviEntitet = context.Uplate.Add(uplata);

            //return mapper.Map<UplataConfirmationDto>(uplata);
            return mapper.Map<UplataConfirmationDto>(noviEntitet.Entity);
        }

        public void DeleteUplata(Guid uplataID)
        {
            Entities.Uplata uplata = GetUplataByID(uplataID);
            context.Uplate.Remove(uplata);
        }

        public List<Entities.Uplata> GetUplate()
        {
            return context.Uplate.ToList();
        }

        public Entities.Uplata GetUplataByID(Guid uplataID)
        {
            return context.Uplate.FirstOrDefault(u => u.UplataID == uplataID);
        }

        public UplataConfirmationDto UpdateUplata(Entities.Uplata uplata)
        {
            throw new NotImplementedException();
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene ce biti perzistirane
        }
    }
}
