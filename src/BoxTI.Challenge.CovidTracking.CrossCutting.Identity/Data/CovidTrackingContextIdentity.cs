using BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BoxTI.Challenge.CovidTracking.CrossCutting.Identity.Data
{
    public class CovidTrackingContextIdentity : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public CovidTrackingContextIdentity(DbContextOptions<CovidTrackingContextIdentity> options) : base(options)
        {
        }
    }
}
