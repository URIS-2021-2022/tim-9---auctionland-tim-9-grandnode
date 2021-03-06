using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Entities;

namespace ZalbaService.Data.Interfaces
{
    public interface IRadnjaRepository
    {
        List<Radnja> GetAllRadnja(string NazivRadnje = null);
        Radnja GetRadnjaById(Guid radnjaId);

        bool SaveChanges();

    }
}
