using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EletronicPartsCatalog.Contracts.DataContracts;

namespace EletronicPartsCatalog.Web.Pages.Models
{
    public class ObjectCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public string ErrorMessage { get; set; }

        public List<PartDto> Parts { get; set; }
    }
}