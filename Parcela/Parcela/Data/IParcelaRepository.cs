using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela.Data
{
    public interface IParcelaRepository
    {
        List<Parcela.Entities.Parcela> GetParcelaList();
        Parcela.Entities.Parcela GetParcelaById(Guid parcelaId);
        Parcela.Entities.Parcela CreateParcela(Parcela.Entities.Parcela parcela);
        Parcela.Entities.Parcela UpdateParcela(Parcela.Entities.Parcela parcela);
        void DeleteParcela(Guid parcelaId);

    }
}
