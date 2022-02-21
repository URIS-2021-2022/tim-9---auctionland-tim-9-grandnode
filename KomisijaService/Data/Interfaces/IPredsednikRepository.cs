using KomisijaService.Entities;
using KomisijaService.Models.Predsednik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Data.Interfaces
{
    public interface IPredsednikRepository
    {
        List<Predsednik> GetAllPredsednik();
        Predsednik GetPredsednikById(Guid predsednikId);
        PredsednikConfirmationDto CreatePredsednik(Predsednik predsednik);
        void DeletePredsednik(Guid predsednikId);
        bool SaveChanges();
    }
}
