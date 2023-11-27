using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class HeroEntity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Numero máximo de caracteres atingido !")]
        public string Name { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Numero máximo de caracteres atingido !")]
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
        public int UserId { get; set; }       
    }
}
