using AutoMapper;
using Kupac_SK.Entities;
using Kupac_SK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Profiles
{
    public class PrioritetProfile : Profile
    {
        public PrioritetProfile()
        {
            CreateMap<PrioritetModel, PrioritetModelDto>();
            CreateMap<PrioritetModelDto, PrioritetModel>();
        }
    }
}
