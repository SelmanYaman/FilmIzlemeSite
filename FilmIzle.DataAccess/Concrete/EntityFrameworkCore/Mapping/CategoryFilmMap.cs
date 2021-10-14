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
    public class CategoryFilmMap : IEntityTypeConfiguration<CategoryFilm>
    {
        public void Configure(EntityTypeBuilder<CategoryFilm> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();

            builder.HasIndex(I => new { I.FilmId, I.CategoryId }).IsUnique();
        }
    }
}
