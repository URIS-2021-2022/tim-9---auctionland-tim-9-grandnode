using AutoMapper;
using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class DeoParceleRepository : IDeoParceleRepository
    {
        public static List<DeoParcele> DeoParceleLista { get; set; } = new List<DeoParcele>();
        public DeoParceleRepository()
        {
            FillData();
        }

        private void FillData()
        {
            DeoParceleLista.AddRange(new List<DeoParcele>
            {
                new DeoParcele
                {
                    DeoParceleID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ParcelaID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    IdealniDeoParcele = 100,
                    StvarniDeoParcele = 100
                }
            }) ;
        }
        public DeoParceleConfirmation CreateDeoParcele(DeoParcele deoParcele)
        {
            deoParcele.DeoParceleID = Guid.NewGuid();
            DeoParceleLista.Add(deoParcele);
            DeoParcele dp = GetDeoParcelaById(deoParcele.DeoParceleID);

            return new DeoParceleConfirmation
            {
                DeoParceleID = dp.DeoParceleID
            };
        }

        public void DeleteDeoParcele(Guid deoParceleId)
        {
            DeoParceleLista.Remove(DeoParceleLista.FirstOrDefault(dp => dp.DeoParceleID == deoParceleId));
        }

        public DeoParcele GetDeoParcelaById(Guid deoParceleId)
        {
            return DeoParceleLista.FirstOrDefault(dp => dp.DeoParceleID == deoParceleId);
        }

        public List<DeoParcele> GetDeoParceleList()
        {
            return DeoParceleLista.ToList();
        }

        public DeoParceleConfirmation UpdateDeoParcele(DeoParcele deoParcele)
        {
            DeoParcele dp = GetDeoParcelaById(deoParcele.DeoParceleID);
            dp.DeoParceleID = deoParcele.DeoParceleID;
            return new DeoParceleConfirmation
            {
                DeoParceleID = dp.DeoParceleID
            };

        }
    }
}
