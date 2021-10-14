using FilmIzle.Business.Interfaces;
using FilmIzle.DataAccess.Interfaces;
using FilmIzle.DTO.DTOs.CategoryDtos;
using FilmIzle.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.Business.Concrete
{
    public class CategoryManager : GenericManager<Category>,ICategoryService
    {
        private readonly IGenericDal<Category> _genericDal;
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(IGenericDal<Category> genericDal, ICategoryDal categoryDal) :base(genericDal)
        {
            _categoryDal = categoryDal;
            _genericDal = genericDal;
        }

        public async Task<Category> FindByCategoryAsync(Category category)
        {
            return await _categoryDal.GetAsync(I => I.Name.ToLower() == category.Name.ToLower());
        }
    }
}
