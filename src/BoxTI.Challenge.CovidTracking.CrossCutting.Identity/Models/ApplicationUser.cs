using Microsoft.AspNetCore.Identity;
using System;

namespace BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }        
    }
}
