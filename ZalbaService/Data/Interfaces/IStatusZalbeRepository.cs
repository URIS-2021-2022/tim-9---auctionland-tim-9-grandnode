using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Entities;

namespace ZalbaService.Data.Interfaces
{
    public interface IStatusZalbeRepository
    {
        List<StatusZalbe> GetAllStatusZalbe(string NazivStatusa = null);
        StatusZalbe GetStatusZalbeById(Guid statusZalbeId);

        bool SaveChanges();

    }
}
