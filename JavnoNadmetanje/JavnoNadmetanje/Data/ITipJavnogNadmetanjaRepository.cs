using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface ITipJavnogNadmetanjaRepository
    {
        List<TipJavnogNadmetanja> GetTipoviJavnogNadmetanja();

        TipJavnogNadmetanja GetTipJavnogNadmetanjaByID(Guid tipJavnogNadmetanjaID);

        TipJavnogNadmetanjaConfirmationDto CreateTipJavnogNadmetanja(TipJavnogNadmetanja tipJavnogNadmetanja);

        TipJavnogNadmetanjaConfirmationDto UpdateTipJavnogNadmetanja(TipJavnogNadmetanja tipJavnogNadmetanja);

        void DeleteTipJavnogNadmetanja(Guid tipJavnogNadmetanjaID);

        bool SaveChanges();
    }
}
