
using EletronicPartsCatalog.Contracts.DataContracts;

namespace EletronicPartsCatalog.DataAccess.Models
{   // Join object for many to many mapping
    public class ObjectPart
    {
        public int ObjectId { get; set; }
        public ObjectDto Object { get; set; }
        public int PartId { get; set; }
        public PartDto Part{ get; set; }


    }
}
