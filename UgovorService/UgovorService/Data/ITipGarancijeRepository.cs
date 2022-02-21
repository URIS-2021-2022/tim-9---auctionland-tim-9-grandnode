using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;

namespace UgovorService.Data
{
    public interface ITipGarancijeRepository
    {
    List<TipGarancijeEnt> GetGarancijes(string Tip = null);

    TipGarancijeEnt GetGarancijeByID(Guid tipID);

    TipGarancijeConfirmation CreateGarancije(TipGarancijeEnt garancija);

    void UpdateGarancije(TipGarancijeEnt garancija);

    void DeleteGarancije(Guid tipID);

    bool SaveChanges();
    }
}
