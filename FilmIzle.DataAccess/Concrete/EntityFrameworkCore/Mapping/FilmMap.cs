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
    public class FilmMap : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();

            builder.Property(I => I.Name).HasMaxLength(100).IsRequired();
            builder.Property(I => I.Description).HasColumnType("ntext").IsRequired();
            builder.Property(I => I.Director).HasMaxLength(100).IsRequired();
            builder.Property(I => I.ImagePath).HasMaxLength(300).IsRequired();
            builder.Property(I => I.ReleaseDate).HasColumnType("date").IsRequired();
            builder.Property(I => I.IMDBPoint).IsRequired().HasPrecision(18, 1);

            builder.HasMany(I => I.Comments).WithOne(I => I.Film).HasForeignKey(I => I.FilmId);

            builder.HasMany(I => I.CategoryFilms).WithOne(I => I.Film).HasForeignKey(I => I.FilmId);
        }
    }
}
