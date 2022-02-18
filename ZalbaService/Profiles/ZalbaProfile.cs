using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Entities;
using ZalbaService.Models.Zalba;

namespace ZalbaService.Profiles
{
    public class ZalbaProfile : Profile
    {

        public ZalbaProfile()
        {
            CreateMap<Zalba, ZalbaDto>();
            CreateMap<ZalbaUpdateDto, Zalba>();
            CreateMap<ZalbaCreationDto, Zalba>();
            CreateMap<Zalba, Zalba>();
        }

    }
}
