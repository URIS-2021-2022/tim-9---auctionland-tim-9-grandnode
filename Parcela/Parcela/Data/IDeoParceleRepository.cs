using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela.Data
{
    public interface IDeoParceleRepository
    {
        List<DeoParcele> GetDeoParceleList();
        DeoParcele GetDeoParcelaById(Guid deoParceleId);
        DeoParcele CreateDeoParcele(DeoParcele deoParcele);
        void UpdateDeoParcele(DeoParcele deoParcele);
        void DeleteDeoParcele(Guid deoParceleId);
        bool SaveChanges();
    }
}
