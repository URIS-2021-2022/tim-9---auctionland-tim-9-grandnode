using AutoMapper;
using KomisijaService.Entities;
using KomisijaService.Models.Clanovi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Profiles
{
    public class ClanoviProfile : Profile
    {
        public ClanoviProfile()
        {
            CreateMap<Clanovi, ClanoviDto>();
            CreateMap<ClanoviUpdateDto, Clanovi>();
            CreateMap<ClanoviCreationDto, Clanovi>();
            CreateMap<Clanovi, Clanovi>();
        }
    }
}
