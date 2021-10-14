using FilmIzle.DTO.DTOs.CategoryDtos;
using FilmIzle.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.Business.Interfaces
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<Category> FindByCategoryAsync(Category category);
    }
}
