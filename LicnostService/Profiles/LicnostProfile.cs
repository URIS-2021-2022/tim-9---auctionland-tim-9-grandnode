using AutoMapper;
using LicnostService.Entities;
using LicnostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicnostService.Profiles
{
    public class LicnostProfile : Profile
    {

        public LicnostProfile()
        {
            CreateMap<Licnost, LicnostDto>()
                .ForMember(dest => dest.ImeLicnosti,
                opt => opt.MapFrom(src => src.Ime + " " + src.Prezime));
            CreateMap<Licnost, LicnostCUDto>();
        }

    }
}
