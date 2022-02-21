using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;
using Uplata.Models;

namespace Uplata.Profiles
{
    public class KursnaListaProfile : Profile
    {
        public KursnaListaProfile()
        {
            CreateMap<KursnaLista, KursnaListaDto>(); //prvo izvor pa destinacija
            CreateMap<KursnaListaCreationDto, KursnaLista>(); //koristi se u post metodi u kontroleru
            CreateMap<KursnaListaUpdateDto, KursnaLista>(); //koristi se u put metodi u kontroleru
            CreateMap<KursnaLista, KursnaLista>(); //koristi se u kontroleru
            CreateMap<KursnaLista, KursnaListaConfirmationDto>(); //koristi se u kontroleru
            CreateMap<KursnaListaConfirmationDto, KursnaListaConfirmationDto>(); //koristi se u kontroleru
        }
    }
}
