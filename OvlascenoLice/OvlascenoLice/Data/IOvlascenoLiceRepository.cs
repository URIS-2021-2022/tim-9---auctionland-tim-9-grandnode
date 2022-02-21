using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OvlascenoLice.Entities;
namespace OvlascenoLice.Data
{
    public interface IOvlascenoLiceRepository
    {
        List<OvlascenoLiceModel> GetOvlascenaLica();

        OvlascenoLiceModel GetOvlascenoLiceById(Guid OLiceID);
        OvlascenoLiceModel CreateOvlascenoLice(OvlascenoLiceModel ovlascenoLice);
        void UpdateOvlascenoLice(OvlascenoLiceModel ovlascenoLice);
        void DeleteOvlascenoLice(Guid OLiceID);

        bool SaveChanges();


    }
}
