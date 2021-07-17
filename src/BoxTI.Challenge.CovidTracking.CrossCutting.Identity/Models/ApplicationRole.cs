using Microsoft.AspNetCore.Identity;
using System;

namespace BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
    }
}
