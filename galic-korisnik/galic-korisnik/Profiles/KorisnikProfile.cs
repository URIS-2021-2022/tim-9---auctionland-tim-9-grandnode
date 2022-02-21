using AutoMapper;
using galic_korisnik.Entities;
using galic_korisnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Profiles
{
    public class KorisnikProfile : Profile
    {
        public KorisnikProfile()
        {
            CreateMap<Korisnik, KorisnikDto>();

            CreateMap<KorisnikDto, Korisnik>();

            CreateMap<KorisnikCreateDto, Korisnik>();
        }
    }
}
