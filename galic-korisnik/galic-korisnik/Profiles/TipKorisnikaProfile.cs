using AutoMapper;
using galic_korisnik.Entities;
using galic_korisnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Profiles
{
    public class TipKorisnikaProfile : Profile
    {
        public TipKorisnikaProfile()
        {
            CreateMap<TipKorisnikaProfile, TipKorisnikaDto>();

            CreateMap<TipKorisnikaDto, TipKorisnika>();

            CreateMap<TipKorisnika, TipKorisnikaDto>();
        }
    }
}
