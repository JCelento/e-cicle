using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletronicPartsCatalog.DataAccess.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
