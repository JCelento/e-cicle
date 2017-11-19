﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EletronicPartsCatalog.DataAccess.Models
{
    public class Object
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Part> Parts { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public ApplicationUser CreatedBy { get; set; }

    }
}