using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Profiles
{
    public class LicitacijaProfile : Profile
    {
        public LicitacijaProfile()
        {
            CreateMap<Licitacija, LicitacijaDto>(); //prvo izvor pa destinacija
            CreateMap<LicitacijaCreationDto, Licitacija>(); //koristi se u post metodi u kontroleru
            CreateMap<LicitacijaUpdateDto, Licitacija>(); //koristi se u put metodi u kontroleru
            CreateMap<Licitacija, Licitacija>(); //koristi se u kontroleru
            CreateMap<Licitacija, LicitacijaConfirmationDto>(); //koristi se u kontroleru
            CreateMap<LicitacijaConfirmationDto, LicitacijaConfirmationDto>(); //koristi se u kontroleru
        }
    }
}
