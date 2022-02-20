using Kupac_SK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Data
{
   public  interface IFizickoLiceRepository
    {
        List<FizickoLice> GetFizickaLica();
        FizickoLice GetFizickoLiceById(Guid flID);
        FizickoLice CreateFizickoLice(FizickoLice fizickoLice);
        void UpdateFizickoLice(FizickoLice fizickoLice);
        void DeleteFizickoLice(Guid flID);


    }
}
