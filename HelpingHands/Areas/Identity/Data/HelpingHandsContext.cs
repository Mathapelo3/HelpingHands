using HelpingHands.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Data;

public class HelpingHandsContext : IdentityDbContext<HelpingHandsUser>
{
    public HelpingHandsContext(DbContextOptions<HelpingHandsContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    
    }
}
