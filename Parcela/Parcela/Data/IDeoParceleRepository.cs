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
<<<<<<< Updated upstream
        DeoParceleConfirmation CreateDeoParcele(DeoParcele deoParcele);
        DeoParceleConfirmation UpdateDeoParcele(DeoParcele deoParcele);
=======
        DeoParcele CreateDeoParcele(DeoParcele deoParcele);
        DeoParcele UpdateDeoParcele(DeoParcele deoParcele);
>>>>>>> Stashed changes
        void DeleteDeoParcele(Guid deoParceleId);
    }
}
