using AutoMapper;
using FilmIzle.Business.Interfaces;
using FilmIzle.DTO.DTOs.CategoryDtos;
using FilmIzle.DTO.DTOs.FilmDtos;
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
    public class HomeController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;
        public HomeController(IFilmService filmService,IMapper mapper)
        {
            _mapper = mapper;
            _filmService = filmService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "Home";
            var resultFilm = await _filmService.GetFilmsSortedDescendingAsync();
            ViewBag.FilmCount = resultFilm.Count;
            return View();
        }
    }
}
