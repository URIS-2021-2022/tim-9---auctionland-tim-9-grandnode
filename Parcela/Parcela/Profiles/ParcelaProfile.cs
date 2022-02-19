using AutoMapper;
using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Profiles
{
    public class ParcelaProfile:Profile
    {
        public ParcelaProfile()
        {
            CreateMap<Parcela.Entities.Parcela, ParcelaDto>();
<<<<<<< Updated upstream
            CreateMap<Parcela.Entities.Parcela, ParcelaCreateDto>();
            CreateMap<Parcela.Entities.Parcela, ParcelaUpdateDto>();
=======
            CreateMap<ParcelaDto, Parcela.Entities.Parcela>();
>>>>>>> Stashed changes

        }
       
    }
}
