using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZalbaService.Entities;
using ZalbaService.Models.TipZalbe;

namespace ZalbaService.Profiles
{
    public class TipZalbeProfile : Profile
    {
        public TipZalbeProfile()
        {
            CreateMap<TipZalbe, TipZalbeDto>();
        }
    }
}
