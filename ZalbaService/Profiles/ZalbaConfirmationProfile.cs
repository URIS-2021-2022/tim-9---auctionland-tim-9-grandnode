using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Entities;
using ZalbaService.Models.Zalba;

namespace ZalbaService.Profiles
{
    public class ZalbaConfirmationProfile : Profile
    {
        public ZalbaConfirmationProfile()
        {
            CreateMap<Zalba, ZalbaConfirmationDto>();
        }
    }
}
