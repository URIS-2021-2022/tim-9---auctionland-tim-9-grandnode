using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Profiles
{
    public class TipJavnogNadmetanjaProfile : Profile
    {
        public TipJavnogNadmetanjaProfile()
        {
            CreateMap<TipJavnogNadmetanja, TipJavnogNadmetanjaDto>(); //prvo izvor pa destinacija
            CreateMap<TipJavnogNadmetanjaCreationDto, TipJavnogNadmetanja>(); //koristi se u post metodi u kontroleru
            CreateMap<TipJavnogNadmetanjaUpdateDto, TipJavnogNadmetanja>(); //koristi se u put metodi u kontroleru
            CreateMap<TipJavnogNadmetanja, TipJavnogNadmetanja>(); //koristi se u put metodi u kontroleru
            CreateMap<TipJavnogNadmetanjaConfirmationDto, TipJavnogNadmetanjaConfirmationDto>(); //koristi se u put metodi u kontroleru
            CreateMap<TipJavnogNadmetanja, TipJavnogNadmetanjaConfirmationDto>(); //koristi se u put metodi u kontroleru
        }
    }
}
