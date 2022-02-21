using AutoMapper;
using Kupac_SK.Entities;
using Kupac_SK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Profiles
{
    public class KontaktOsobaProfile : Profile
    {
        public KontaktOsobaProfile()
        {
            CreateMap<KontaktOsobaModel, KontaktOsobaDto>();
            CreateMap<KontaktOsobaDto, KontaktOsobaModel>();
        }
    }
}
