using FilmIzle.DataAccess.Concrete.EntityFrameworkCore.Context;
using FilmIzle.DataAccess.Interfaces;
using FilmIzle.DTO.DTOs.CategoryDtos;
using FilmIzle.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCategoryRepository : EfGenericRepository<Category>, ICategoryDal
    {
        public EfCategoryRepository(FilmContext context):base(context)
        {

        }
    }
}
