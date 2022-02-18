using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Entities;
using ZalbaService.Models.Radnja;

namespace ZalbaService.Profiles
{
    public class RadnjaProfile : Profile
    {

        public RadnjaProfile()
        {
            CreateMap<Radnja, RadnjaDto>();
            CreateMap<RadnjaUpdateDto, Radnja>();
            CreateMap<RadnjaCreationDto, Radnja>();
            CreateMap<Radnja, Radnja>();
        }

    }
}
