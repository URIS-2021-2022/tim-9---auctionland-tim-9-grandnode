﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Models
{
    public class DeoParceleCreateDto : IValidatableObject
    {
        
        [Required]
        public Guid ParcelaID { get; set; }
        public int IdealniDeoParcele { get; set; }
        public int StvarniDeoParcele { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
