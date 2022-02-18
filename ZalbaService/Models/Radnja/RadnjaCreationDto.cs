﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZalbaService.Models.Radnja
{
    public class RadnjaCreationDto
    {

        [Required(ErrorMessage = "Obavezno uneti naziv radnje na osnovu zalbe!")]
        public string NazivRadnje { get; set; }

    }
}
