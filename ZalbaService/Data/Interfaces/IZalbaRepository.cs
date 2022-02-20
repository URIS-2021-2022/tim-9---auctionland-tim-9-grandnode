using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Entities;
using ZalbaService.Models.Zalba;

namespace ZalbaService.Data.Interfaces
{
    public interface IZalbaRepository
    {
        List<Zalba> GetAllZalba();
        Zalba GetZalbaById(Guid zalbaId);
        ZalbaConfirmationDto CreateZalba(Zalba zalba);
        void UpdateZalba(Zalba zalba);
        void DeleteZalba(Guid zalbaId);
        bool SaveChanges();

    }
}
