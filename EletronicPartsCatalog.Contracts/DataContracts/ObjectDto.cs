using System;
using System.Collections.Generic;
using System.Text;

namespace EletronicPartsCatalog.Contracts.DataContracts
{
    public class ObjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ObjectPartDto> ObjectParts { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
