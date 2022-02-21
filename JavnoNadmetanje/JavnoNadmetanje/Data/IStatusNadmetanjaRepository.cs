using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface IStatusNadmetanjaRepository
    {
        List<StatusNadmetanja> GetStatusiNadmetanja();

        StatusNadmetanja GetStatusNadmetanjaByID(Guid statusNadmetanjaID);

        StatusNadmetanjaConfirmationDto CreateStatusNadmetanja(StatusNadmetanja statusNadmetanja);

        StatusNadmetanjaConfirmationDto UpdateStatusNadmetanja(StatusNadmetanja statusNadmetanja);

        void DeleteStatusNadmetanja(Guid statusNadmetanjaID);

        bool SaveChanges();
    }
}
