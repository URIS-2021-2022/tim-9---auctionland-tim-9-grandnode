using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Entities;

namespace ZalbaService.Data.Interfaces
{
    public interface ITipZalbeRepository
    {

        List<TipZalbe> GetAllTipZalbe(string NazivTipa = null);
        TipZalbe GetTipZalbeById(Guid tipZalbeId);
        TipZalbe CreateTipZalbe(TipZalbe tipZalbe);
        void UpdateTipZalbe(TipZalbe tipZalbe);
        void DeleteTipZalbe(Guid tipZalbeId);
        bool SaveChanges();

    }
}
