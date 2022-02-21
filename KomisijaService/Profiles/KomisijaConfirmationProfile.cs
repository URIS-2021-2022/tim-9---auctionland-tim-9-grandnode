using AutoMapper;
using KomisijaService.Entities;
using KomisijaService.Models.Komisija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Profiles
{
    public class KomisijaConfirmationProfile : Profile
    {
        public KomisijaConfirmationProfile()
        {
            CreateMap<Komisija, KomisijaConfirmationDto>();
        }
    }
}
