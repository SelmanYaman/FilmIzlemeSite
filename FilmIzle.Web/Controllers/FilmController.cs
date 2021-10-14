using AutoMapper;
using FilmIzle.Business.Interfaces;
using FilmIzle.DTO.DTOs.CommentDtos;
using FilmIzle.DTO.DTOs.FilmDtos;
using FilmIzle.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIzle.Web.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        public FilmController(IFilmService filmService, IMapper mapper, ICommentService commentService)
        {
            _commentService = commentService;
            _mapper = mapper;
            _filmService = filmService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            Tuple<List<Film>, int> sonuc = await _filmService.GetFilmsSortedDescendingAsync(page);
            ViewBag.filmPage = page;
            ViewBag.filmPageCount = sonuc.Item2;
            ViewBag.Action = "Index";
            return View(_mapper.Map<List<FilmListDto>>(sonuc.Item1));
        }
        public async Task<IActionResult> Category(string name, int page = 1)
        {
            Tuple<List<Film>, int> sonuc = await _filmService.GetFilmByCategoryAsync(name, page);
            ViewBag.filmPage = page;
            ViewBag.filmPageCount = sonuc.Item2;
            ViewBag.Action = "Category";
            return View(_mapper.Map<List<FilmListDto>>(sonuc.Item1));
        }
        public async Task<IActionResult> Watch(string name, string language)
        {
            if (language == "TRD")
            {
                TempData["Language"] = "TRD";
            }
            else if (language == "TRA")
            {
                TempData["Language"] = "TRA";
            }
            else
            {
                TempData["Language"] = "TRD";
            }

            if (language == null)
            {
                await _filmService.UpdateClickFilm(name);
            }

            var categories = await _filmService.GetCategoriesOfFilmAsync(name);
            string resultCategories = null;
            for (int i = 0; i < categories.Count; i++)
            {
                if (i < categories.Count - 1)
                {
                    resultCategories += categories[i].Name.ToString() + " - ";
                }
                else
                {
                    resultCategories += categories[i].Name.ToString();
                }
            }


            var filmWatch = await _filmService.GetFilmWatchAsync(name);
            int? parentId = null;
            ViewBag.Comments = _mapper.Map<List<CommentListDto>>(await _commentService.GetAllWithSubCommentsAsync(filmWatch.Id, parentId));

            TempData["FilmName"] = name;
            TempData["Categories"] = resultCategories;
            return View(_mapper.Map<FilmWatchDto>(filmWatch));
        }

        [HttpPost]
        public async Task<IActionResult> AddToComment(CommentAddDto model)
        {
            model.PostedTime = DateTime.Now;
            await _commentService.AddAsync(_mapper.Map<Comment>(model));
            return RedirectToAction("Watch", new { name = TempData["FilmName"] });
        }

        public async Task<IActionResult> MostClicked(int page = 1)
        {
            Tuple<List<Film>, int> sonuc = await _filmService.GetFilmMostClickedAsync(page);
            ViewBag.filmPage = page;
            ViewBag.filmPageCount = sonuc.Item2;
            ViewBag.Action = "MostClicked";
            return View(_mapper.Map<List<FilmListDto>>(sonuc.Item1));
        }

        public async Task<IActionResult> MostCommented(int page = 1)
        {
            Tuple<List<Film>, int> sonuc = await _filmService.GetFilmMostCommentedAsync(page);
            ViewBag.filmPage = page;
            ViewBag.filmPageCount = sonuc.Item2;
            ViewBag.Action = "MostCommented";
            return View(_mapper.Map<List<FilmListDto>>(sonuc.Item1));
        }

        public async Task<IActionResult> ImdbSevenPoint(int page = 1)
        {
            Tuple<List<Film>, int> sonuc = await _filmService.GetFilmImdbPointAsync(page);
            ViewBag.filmPage = page;
            ViewBag.filmPageCount = sonuc.Item2;
            ViewBag.Action = "ImdbSevenPoint";
            return View(_mapper.Map<List<FilmListDto>>(sonuc.Item1));
        }

        public async Task<IActionResult> Year2021(int page = 1)
        {
            Tuple<List<Film>, int> sonuc = await _filmService.GetFilmYear2021Async(page);
            ViewBag.filmPage = page;
            ViewBag.filmPageCount = sonuc.Item2;
            ViewBag.Action = "Year2021";
            return View(_mapper.Map<List<FilmListDto>>(sonuc.Item1));
        }

        public async Task<IActionResult> TRDubbing(int page = 1)
        {
            Tuple<List<Film>, int> sonuc = await _filmService.GetFilmTRDubbingAsync(page);
            ViewBag.filmPage = page;
            ViewBag.filmPageCount = sonuc.Item2;
            ViewBag.Action = "TRDubbing";
            return View(_mapper.Map<List<FilmListDto>>(sonuc.Item1));
        }

        public async Task<IActionResult> TRSubtitle(int page = 1)
        {
            Tuple<List<Film>, int> sonuc = await _filmService.GetFilmTRSubtitleAsync(page);
            ViewBag.filmPage = page;
            ViewBag.filmPageCount = sonuc.Item2;
            ViewBag.Action = "TRSubtitle";
            return View(_mapper.Map<List<FilmListDto>>(sonuc.Item1));
        }

        public async Task<IActionResult> Search(string q, int page = 1)
        {
            if (q != null)
            {
                Tuple<List<Film>, int> sonuc = await _filmService.GetFilmSearchAsync(q, page);
                ViewBag.filmPage = page;
                ViewBag.filmPageCount = sonuc.Item2;
                ViewBag.SearchFilm = q;
                ViewBag.Action = "Index";
                return View(_mapper.Map<List<FilmListDto>>(sonuc.Item1));
            }
            return RedirectToAction("Index");
        }
    }
}
