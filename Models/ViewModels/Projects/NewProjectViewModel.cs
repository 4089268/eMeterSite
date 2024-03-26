using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMeterSite.Models.ViewModels.Projects
{
    public class NewProjectViewModel
    {   
        [Required(ErrorMessage = "El nombre del proyecto es requerido.")]
        public string? Proyecto {get;set;}

        [Required(ErrorMessage = "La clave del proyecto es requerido.")]
        public string? Clave {get;set;}
    }
}