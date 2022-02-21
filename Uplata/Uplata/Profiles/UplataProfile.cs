using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;
using Uplata.Models;

namespace Uplata.Profiles
{
    public class UplataProfile : Profile
    {
        public UplataProfile()
        {
            CreateMap<Entities.Uplata, UplataDto>(); //prvo izvor pa destinacija
            CreateMap<UplataCreationDto, Entities.Uplata>(); //koristi se u post metodi u kontroleru
            CreateMap<UplataUpdateDto, Entities.Uplata>(); //koristi se u put metodi u kontroleru
            CreateMap<Entities.Uplata, Entities.Uplata>(); //koristi se u kontroleru
            CreateMap<Entities.Uplata, UplataConfirmationDto>(); //koristi se u kontroleru
            CreateMap<UplataConfirmationDto, UplataConfirmationDto>(); //koristi se u kontroleru
        }
    }
}
