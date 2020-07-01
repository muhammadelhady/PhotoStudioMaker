using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.DTO
{
    public class UserForRegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(8,MinimumLength =4,ErrorMessage ="the password legnth must be betweem 4 and 8")]
        public string Password { get; set; }
    }
}
