﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Entities
{
    public class InterniDokumentConfirmation
    {
        public Guid DokumentID { get; set; }
        public bool Izmenjen { get; set; }
    }
}
