﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZalbaService.Models.StatusZalbe
{
    public class StatusZalbeCreationDto
    {
        [Required(ErrorMessage = "Obavezno uneti naziv statusa zalbe!")]
        public string NazivStatusa { get; set; }
    }
}
