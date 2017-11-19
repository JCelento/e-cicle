using System;

namespace EletronicPartsCatalog.Contracts.DataContracts
{
    public class AddProjectDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
    }
}
