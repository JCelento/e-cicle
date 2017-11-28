﻿using System;
using System.Collections.Generic;

namespace EletronicPartsCatalog.Contracts.DataContracts
{
    public class AddProjectDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public List<PartDto> Parts { get; set; }
    }
}
