using AutoMapper;
using KomisijaService.Entities;
using KomisijaService.Models.Predsednik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Profiles
{
    public class PredsednikProfile : Profile
    {
        public PredsednikProfile()
        {
            CreateMap<Predsednik, PredsednikDto>();
            CreateMap<PredsednikCreationDto, Predsednik>();
            CreateMap<Predsednik, Predsednik>();
        }
    }
}
