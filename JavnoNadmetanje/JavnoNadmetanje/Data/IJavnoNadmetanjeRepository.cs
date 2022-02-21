using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Data
{
    public interface IJavnoNadmetanjeRepository
    {
        List<Entities.JavnoNadmetanje> GetJavnaNadmetanja();

        Entities.JavnoNadmetanje GetJavnoNadmetanjeByID(Guid javnoNadmetanjeID);

        JavnoNadmetanjeConfirmationDto CreateJavnoNadmetanje(Entities.JavnoNadmetanje javnoNadmetanje);

        JavnoNadmetanjeConfirmationDto UpdateJavnoNadmetanje(Entities.JavnoNadmetanje javnoNadmetanje);

        void DeleteJavnoNadmetanje(Guid javnoNadmetanjeID);

        bool SaveChanges();
    }
}
