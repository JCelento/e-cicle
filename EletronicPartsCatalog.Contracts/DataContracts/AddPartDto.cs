using System.Collections.Generic;

namespace EletronicPartsCatalog.Contracts.DataContracts
{
    public class AddPartDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public List<ObjectPartDto> PartObjects { get; set; }
    }
}
