using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class ParcelaRepository : IParcelaRepository
    {
<<<<<<< Updated upstream
        public ParcelaConfrimation CreateParcela(Entities.Parcela parcela)
=======
        public Parcela.Entities.Parcela CreateParcela(Entities.Parcela parcela)
>>>>>>> Stashed changes
        {
            throw new NotImplementedException();
        }

        public void DeleteParcela(Guid parcelaId)
        {
            throw new NotImplementedException();
        }

        public Entities.Parcela GetParcelaById(Guid parcelaId)
        {
            throw new NotImplementedException();
        }

        public List<Entities.Parcela> GetParcelaList()
        {
            throw new NotImplementedException();
        }

<<<<<<< Updated upstream
        public ParcelaConfrimation UpdateParcela(Entities.Parcela parcela)
=======
        public Parcela.Entities.Parcela UpdateParcela(Entities.Parcela parcela)
>>>>>>> Stashed changes
        {
            throw new NotImplementedException();
        }
    }
}
