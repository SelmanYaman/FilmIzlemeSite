using FilmIzle.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();

            builder.Property(I => I.UserName).HasMaxLength(100).IsRequired();
            builder.Property(I => I.Password).HasMaxLength(100).IsRequired();
            builder.Property(I => I.Email).HasMaxLength(100);
            builder.Property(I => I.Name).HasMaxLength(100);
            builder.Property(I => I.SurName).HasMaxLength(100);

            builder.HasData(new AppUser
            {
                Id = 1,
                Name = "Admin",
                SurName = "Admin",
                Email = "Admin@gmail.com",
                UserName = "admin",
                Password = "123"
            });

            builder.HasMany(I => I.Films).WithOne(I => I.AppUser).HasForeignKey(I => I.AppUserId);
        }
    }
}
