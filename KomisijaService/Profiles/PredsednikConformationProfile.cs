using AutoMapper;
using KomisijaService.Entities;
using KomisijaService.Models.Predsednik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomisijaService.Profiles
{
    public class PredsednikConformationProfile : Profile
    {
        public PredsednikConformationProfile()
        {
            CreateMap<Predsednik, PredsednikConfirmationDto>();
        }
    }
}
