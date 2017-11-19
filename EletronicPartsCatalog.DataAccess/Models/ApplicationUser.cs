using System;
using Microsoft.AspNetCore.Identity;

namespace EletronicPartsCatalog.DataAccess.Models
{

    public class ApplicationUser : IdentityUser
    {
        public string Id { get; set; }
        
    }
}
