using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;
using UgovorService.Models;

namespace UgovorService.Profiles
{
    public class TipGarancijeProfile : Profile
    {
        public TipGarancijeProfile()
        {
            CreateMap<TipGarancijeEnt, TipGarancijeDto>();
            CreateMap<TipGarancijeCreationDto, TipGarancijeEnt>();
            CreateMap<TipGarancijeEnt, TipGarancijeUpdateDto>();
        }
    }
}
