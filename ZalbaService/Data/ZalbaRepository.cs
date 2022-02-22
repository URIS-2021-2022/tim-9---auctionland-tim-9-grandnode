using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Data.Interfaces;
using ZalbaService.Entities;
using ZalbaService.Entities.DataContext;
using ZalbaService.Models.Zalba;

namespace ZalbaService.Data
{
    public class ZalbaRepository : IZalbaRepository
    {

        private readonly ZalbaContext context;
        private readonly IMapper mapper;

        public ZalbaRepository(ZalbaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ZalbaConfirmationDto CreateZalba(Zalba zalba)
        {
            var createdEntity = context.Add(zalba);
            return mapper.Map<ZalbaConfirmationDto>(createdEntity.Entity);
        }

        public void DeleteZalba(Guid zalbaId)
        {
            var zalba = GetZalbaById(zalbaId);
            context.Remove(zalba);
        }

        public List<Zalba> GetAllZalba()
        {
            return context.Zalba.ToList();
        }

        public Zalba GetZalbaById(Guid zalbaId)
        {
            return context.Zalba.FirstOrDefault(z => z.ZalbaId == zalbaId);
        }

        public void UpdateZalba(Zalba zalba)
        {
            //Entity framework core prati entitet pa nema potrebe za implementacijom
        }
    }
}
