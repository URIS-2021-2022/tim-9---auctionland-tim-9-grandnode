using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Profiles
{
    public class JavnoNadmetanjeProfile : Profile
    {
        public JavnoNadmetanjeProfile()
        {
            CreateMap<Entities.JavnoNadmetanje, JavnoNadmetanjeDto>(); //prvo izvor pa destinacija
            CreateMap<JavnoNadmetanjeCreationDto, Entities.JavnoNadmetanje>(); //koristi se u post metodi u kontroleru
            CreateMap<JavnoNadmetanjeUpdateDto, Entities.JavnoNadmetanje>(); //koristi se u put metodi u kontroleru
            CreateMap<Entities.JavnoNadmetanje, Entities.JavnoNadmetanje>(); //koristi se u kontroleru
            CreateMap<Entities.JavnoNadmetanje, JavnoNadmetanjeConfirmationDto>(); //koristi se u kontroleru
            CreateMap<JavnoNadmetanjeConfirmationDto, JavnoNadmetanjeConfirmationDto>(); //koristi se u kontroleru
        }
    }
}
