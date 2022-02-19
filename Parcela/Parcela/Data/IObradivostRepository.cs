using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcela.Entities;

namespace Parcela.Data
{
    public interface IObradivostRepository
    {
        List<Obradivost> GetObradivostList();
        Obradivost GetObradivostById(Guid obradivostId);
    }
}
