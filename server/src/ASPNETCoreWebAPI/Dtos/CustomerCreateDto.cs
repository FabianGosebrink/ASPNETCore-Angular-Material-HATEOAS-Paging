using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreWebAPI.Dtos
{
    public class CustomerCreateDto
    {
        [Required]
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
}
