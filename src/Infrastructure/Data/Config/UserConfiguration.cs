using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.VkontakteId);
            builder.Property(u => u.VkontakteId)
                .ValueGeneratedNever();

            builder.Property(u => u.FirstName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorVkId);
        }
    }
}
