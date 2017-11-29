using System;
using System.Collections.Generic;
using System.Text;
using EletronicPartsCatalog.Contracts.DataContracts;

namespace EletronicPartsCatalog.DataAccess.Models
{
    public class Object
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
