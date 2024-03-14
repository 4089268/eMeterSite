using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMeterSite.Models.ViewModels
{
    public class AuthenticationViewModel
    {

        [Required]
        public string User {get; set;} = null!;
        
        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;} = null!;
        
    }
}