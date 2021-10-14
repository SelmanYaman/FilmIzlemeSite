using AutoMapper;
using FilmIzle.Business.Interfaces;
using FilmIzle.DTO.DTOs.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIzle.Web.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryList(ICategoryService categoryService, IMapper mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_mapper.Map<List<CategoryListDto>>(await _categoryService.GetAllAsync()));
        }
    }
}
