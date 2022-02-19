using AutoMapper;
using Parcela.Models;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Profiles
{
    public class ObradivostProfile : Profile
    {
        public ObradivostProfile()
        {
            CreateMap<Obradivost, ObradivostDto>();
        }
    }
}
