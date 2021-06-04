using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeSheets.Models;

namespace TimeSheets.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id");

            builder.OwnsMany(
               p => p.RefreshTokens,
               a =>
               {
                   a.WithOwner().HasForeignKey("CreatedBy");
                   a.Property<int>("Id");
                   a.HasKey("Id");
               });
        }
    }
}