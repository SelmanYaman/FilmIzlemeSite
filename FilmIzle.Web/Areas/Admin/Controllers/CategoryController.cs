using AutoMapper;
using FilmIzle.Business.Interfaces;
using FilmIzle.DTO.DTOs.CategoryDtos;
using FilmIzle.DTO.DTOs.FilmDtos;
using FilmIzle.Entities.Concrete;
using FilmIzle.Web.Filter;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIzle.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [UserFilter]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService,IMapper mapper,IFilmService filmService)
        {
            _filmService = filmService;
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "Category";
            return View(_mapper.Map<List<CategoryListDto>>(await _categoryService.GetAllAsync()));
        }
        public IActionResult AddCategory()
        {
            TempData["Active"] = "Category";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryAddDto model)
        {
            TempData["Active"] = "Category";

            if (ModelState.IsValid)
            {
                var category = await _categoryService.FindByCategoryAsync(new Category { Name = model.Name });
                if(category == null)
                {
                    await _categoryService.AddAsync(new Category
                    {
                        Name = model.Name
                    });
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("","Böyle Bir Kategori Bulunmaktadır.");
                }
                
            }
            return View(model);
        }
        public async Task<IActionResult> UpdateCategory(int id)
        {
            TempData["Active"] = "Category";

            var categoryName = await _categoryService.FindByIdAsync(id);
            return View(_mapper.Map<CategoryUpdateDto>(categoryName));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateDto model)
        {
            TempData["Active"] = "Category";

            if (ModelState.IsValid)
            {
                var categoryName = await _categoryService.FindByCategoryAsync(new Category { Name = model.Name });
                if (categoryName == null)
                {
                    await _categoryService.UpdateAsync(new Category
                    {
                        Id = model.Id,
                        Name = model.Name
                    });
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Böyle Bir Kategori Bulunmaktadır.");
                }

            }
            return View(model);
        }
        public async Task<IActionResult> RemoveCategory(int id)
        {
            TempData["Active"] = "Category";

            await _categoryService.RemoveAsync(new Category
            {
                Id = id
            });
            return Json(null);
        }

        public async Task<IActionResult> NumberOfFilmsByCategory()
        {
            TempData["Active"] = "Category";

            var categoryFilmCount = await _filmService.GetNumberOfFilmsByCategoryAsync();

            List<CategoryFilmCountListDto> list = new();
            foreach (var item in categoryFilmCount)
            {
                CategoryFilmCountListDto model = new CategoryFilmCountListDto();
                model.Count = item.Id;
                model.Name = item.Name;
                list.Add(model);
            }
            return View(list);
        }

        public async Task<IActionResult> FilmByCategory(string q)
        {
            Tuple<List<Film>, int> sonuc = await _filmService.GetFilmByCategoryAsync(q, 1);
            TempData["Active"] = "Category";
            ViewBag.Category = q;
            return View(_mapper.Map<List<FilmListAllDto>>(sonuc.Item1));
        }
    }
}
