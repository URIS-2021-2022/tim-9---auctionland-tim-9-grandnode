using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcela.Entities;
using Parcela.Models;

namespace Parcela.Profiles
{
    public class DeoParceleConfrimationProfile : Profile
    {
        public DeoParceleConfrimationProfile()
        {
            CreateMap<DeoParceleConfirmation, ParceleConfrimationDto>();
        }
    }
}
