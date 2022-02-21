﻿using AutoMapper;
using Dokument_AK.Entities;
using Dokument_AK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Profiles
{
    public class StatusDokumentaConfirmationProfile : Profile
    {
        public StatusDokumentaConfirmationProfile()
        {
            CreateMap<StatusDokumentaConfirmation, StatusDokumentaConfirmationDto>();
        }
    }
}
