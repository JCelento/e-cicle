using System;
using System.Collections.Generic;
using System.Text;

namespace EletronicPartsCatalog.Contracts.DataContracts
{
    public class ObjectPartDto
    {

        public int ObjectId { get; set; }
        public ObjectDto Object { get; set; }
        public int PartId { get; set; }
        public PartDto Part { get; set; }

    }
}
