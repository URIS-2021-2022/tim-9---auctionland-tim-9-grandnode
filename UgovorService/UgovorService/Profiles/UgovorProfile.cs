using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;
using UgovorService.Models;

namespace UgovorService.Profiles
{
    public class UgovorProfile : Profile
    {
        public UgovorProfile()
        {
            CreateMap<UgovorEnt, UgovorDto>();
            CreateMap<UgovorCreationDto, UgovorEnt>();
            CreateMap<UgovorEnt, UgovorUpdateDto>();
            CreateMap<UgovorEnt, UgovorEnt>();
        }
    }
}
