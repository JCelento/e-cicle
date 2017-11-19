using System.ComponentModel.DataAnnotations;

namespace EletronicPartsCatalog.Web.Pages.Models
{
    public class ProjectCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public string ErrorMessage { get; set; }
    }
}