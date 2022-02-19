using AutoMapper;
using Mesto.Entities;
using Mesto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesto.Profiles
{
    public class AdresaProfile : Profile
    {
        public AdresaProfile()
        {
            CreateMap<Adresa, AdresaDto>();
            CreateMap<AdresaDto, Adresa>();

        }
    }
}
