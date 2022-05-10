using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace siges.Models.ViewModels
{
    public class SupportViewModel
    {
        [Required (ErrorMessage ="Este campo es requerido")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string LastName { get; set; }
        [EmailAddress (ErrorMessage = "Email invalido")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Priority { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(200, MinimumLength = 40, ErrorMessage = "Debe detallar más el problema")]
        public string Body { get; set; }
        [NotMapped]
        public IList<IFormFile> Screenshots { get; set; }
    }
}
