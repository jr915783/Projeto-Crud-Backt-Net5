using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class UserLoginDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Numero máximo de caracteres atingido !")]
        public string UserName { get; set; }
        [Required]        
        public string Password { get; set; }
    }
}
