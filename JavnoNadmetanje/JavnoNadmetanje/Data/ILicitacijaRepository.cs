using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface ILicitacijaRepository
    {
        List<Licitacija> GetLicitacije();

        Licitacija GetLicitacijaByID(Guid licitacijaID);

        LicitacijaConfirmationDto CreateLicitacija(Licitacija licitacija);

        LicitacijaConfirmationDto UpdateLicitacija(Licitacija licitacija);

        void DeleteLicitacija(Guid licitacijaID);

        bool SaveChanges();
    }
}
