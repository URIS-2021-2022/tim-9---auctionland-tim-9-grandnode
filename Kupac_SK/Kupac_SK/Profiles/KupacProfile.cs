using AutoMapper;
using Kupac_SK.Entities;
using Kupac_SK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Profiles
{
    public class KupacProfile : Profile 
    {
        public KupacProfile()
        {
            CreateMap<KupacModel, KupacModelDto>();
            CreateMap<KupacModelDto, KupacModel>();
            CreateMap<KupacConfirmationDto, KupacConfirmation>();
            CreateMap<KupacConfirmation, KupacConfirmationDto>();
        }
    }
}
