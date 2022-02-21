using AutoMapper;
using OvlascenoLice.Entities;
using OvlascenoLice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvlascenoLice.Data
{
    public class OvlascenoLiceRepository : IOvlascenoLiceRepository
    {
        public static List<OvlascenoLiceModel> Lica { get; set; } = new List<OvlascenoLiceModel>();

        private readonly OvlascenoLiceContext context;
        private readonly IMapper mapper;


        public OvlascenoLiceRepository(OvlascenoLiceContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
   
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        private void FillData()
        {
            Lica.AddRange(new List<OvlascenoLiceModel>{
                new OvlascenoLiceModel
                {
                    OvlascenoLiceID = Guid.Parse("5dc3dfcd-de07-4e5f-878e-a07636db322f"),
                    Ime ="Sara",
                    Prezime ="Kijanovic",
                    BrojDokumenta = "4585248",
                    BrojTable = "74474",
                    AdresaID = Guid.Parse("7280c84a-a070-4516-94e7-ef905c7dcf8b")
                },
                new OvlascenoLiceModel
                {
                    OvlascenoLiceID = Guid.Parse("668e0c43-810b-4443-82a7-649b4f25a840"),
                    Ime ="Marko",
                    Prezime ="Ruzic",
                    BrojDokumenta = "465548",
                    BrojTable = "7434664",
                    AdresaID = Guid.Parse("4ead0649-3ad7-42cb-92b3-80e504006df9")

                }


            });
        }
        public OvlascenoLiceModel CreateOvlascenoLice(OvlascenoLiceModel ovlascenoLice)
        {
            var createdEntity = context.Add(ovlascenoLice);
            return mapper.Map<OvlascenoLiceModel>(createdEntity.Entity);
        }

        public void DeleteOvlascenoLice(Guid OLiceID)
        {
            //Lica.Remove(Lica.FirstOrDefault(e => e.OvlascenoLiceID == OLiceID));
            var ovlLice = GetOvlascenoLiceById(OLiceID);
            context.Remove(ovlLice);

        }

        public List<OvlascenoLiceModel> GetOvlascenaLica()
        {
            return context.OvlascenaLica.ToList();
        }

        public OvlascenoLiceModel GetOvlascenoLiceById(Guid OLiceID)
        {
            return context.OvlascenaLica.FirstOrDefault(e => e.OvlascenoLiceID == OLiceID);
        }

        public void UpdateOvlascenoLice(OvlascenoLiceModel ovlascenoLice)
        {
          //
        }
    }
}
