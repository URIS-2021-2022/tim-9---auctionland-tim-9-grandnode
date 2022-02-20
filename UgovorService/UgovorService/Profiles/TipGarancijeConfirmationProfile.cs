using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;
using UgovorService.Models;

namespace UgovorService.Profiles
{
    public class TipGarancijeConfirmationProfile : Profile
    {
        public TipGarancijeConfirmationProfile()
        {
            CreateMap<TipGarancijeConfirmation, TipGarancijeConfirmationDto>();
            CreateMap<TipGarancijeConfirmation, TipGarancijeUpdateDto>();
        }
    }
}
