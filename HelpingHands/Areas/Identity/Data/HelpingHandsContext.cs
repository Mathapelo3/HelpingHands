using HelpingHands.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.ApplyConfiguration(new HelpingHandsContextEntityConfiguration());
    }

    public class HelpingHandsContextEntityConfiguration : IEntityTypeConfiguration<HelpingHandsUser>
    {
        public void Configure(EntityTypeBuilder<HelpingHandsUser> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(20);
            builder.Property(x => x.Surname).HasMaxLength(30);
        }
    }
}
