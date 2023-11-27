using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Numero máximo de caracteres atingido !")]
        public string UserName { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Numero minímo de caracteres são 4 !")]
        public string Password { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Numero máximo de caracteres atingido !")]
        [EmailAddress(ErrorMessage = "E-mail inválido !")]
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
