using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcela.Entities;

namespace Parcela.Data
{
    public interface IKlasaRepository
    {
        List<Klasa> GetKlasaList();
        Klasa GetKlasaById(Guid klasaId);
    }
}
