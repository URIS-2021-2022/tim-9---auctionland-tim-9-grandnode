using AutoMapper;
using Mesto.Entities;
using Mesto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesto.Profiles
{
    public class DrzavaProfile : Profile
    {
        public DrzavaProfile()
        {
            CreateMap<Drzava, DrzavaDto>();
            CreateMap<DrzavaDto, Drzava>();
        }
    }
}
