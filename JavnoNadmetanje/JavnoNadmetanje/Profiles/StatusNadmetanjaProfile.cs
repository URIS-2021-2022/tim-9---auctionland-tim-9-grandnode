using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Profiles
{
    public class StatusNadmetanjaProfile : Profile
    {
        public StatusNadmetanjaProfile()
        {
            CreateMap<StatusNadmetanja, StatusNadmetanjaDto>(); //prvo izvor pa destinacija
            CreateMap<StatusNadmetanjaCreationDto, StatusNadmetanja>(); //koristi se u post metodi u kontroleru
            CreateMap<StatusNadmetanjaUpdateDto, StatusNadmetanja>(); //koristi se u put metodi u kontroleru
            CreateMap<StatusNadmetanja, StatusNadmetanja>(); //koristi se u kontroleru
            CreateMap<StatusNadmetanja, StatusNadmetanjaConfirmationDto>(); //koristi se u kontroleru
            CreateMap<StatusNadmetanjaConfirmationDto, StatusNadmetanjaConfirmationDto>(); //koristi se u kontroleru
        }
    }
}
