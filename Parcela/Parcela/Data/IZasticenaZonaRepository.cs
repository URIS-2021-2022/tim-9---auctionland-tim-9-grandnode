using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcela.Entities;

namespace Parcela.Data
{
    public interface IZasticenaZonaRepository
    {
        List<ZasticenaZona> GetZasticenaZonaList();
        ZasticenaZona GetZasticenaZonaById(Guid zasticenaZonaId);
    }
}
