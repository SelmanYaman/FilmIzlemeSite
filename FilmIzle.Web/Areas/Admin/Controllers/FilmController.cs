using AutoMapper;
using FilmIzle.Business.Interfaces;
using FilmIzle.DTO.DTOs.CategoryDtos;
using FilmIzle.DTO.DTOs.CategoryFilmDtos;
using FilmIzle.DTO.DTOs.FilmDtos;
using FilmIzle.Entities.Concrete;
using FilmIzle.Web.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIzle.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [UserFilter]
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public FilmController(IFilmService filmService, IMapper mapper, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _filmService = filmService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            TempData["Active"] = "Film";
            Tuple<List<Film>, int> sonuc = await _filmService.GetFilmsSortedDescendingAsync(page);
            ViewBag.Page = page;
            ViewBag.PageCount = sonuc.Item2;
            return View(_mapper.Map<List<FilmListAllDto>>(sonuc.Item1));

            //var resultFilm = await _filmService.GetFilmsSortedDescendingAsync();
            //var filmCount = resultFilm.Count;
            //var filmPageCount = (int)Math.Ceiling((double)filmCount / 20);
            //ViewBag.Page = page;
            //ViewBag.PageCount = filmPageCount;
            //resultFilm = resultFilm.Skip((page - 1) * 20).Take(20).ToList();
            //return View(_mapper.Map<List<FilmListAllDto>>(resultFilm));
        }

        public async Task<IActionResult> Search(string q, int page = 1)
        {
            TempData["Active"] = "Film";
            if (q != null)
            {
                Tuple<List<Film>, int> sonuc = await _filmService.GetFilmSearchAsync(q, page);
                ViewBag.SearchPage = page;
                ViewBag.SearchPageCount = sonuc.Item2;
                ViewBag.SearchFilm = q;
                return View(_mapper.Map<List<FilmListAllDto>>(sonuc.Item1));
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddFilm()
        {
            TempData["Active"] = "Film";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFilm(FilmAddDto model, IFormFile image)
        {
            TempData["Active"] = "Film";
            if (ModelState.IsValid)
            {
                if (model.TRDubbing == false && model.TRSubtitle == false)
                {
                    ModelState.AddModelError("", "En az bir dil seçmelisiniz.");
                }
                else if (image == null)
                {
                    ModelState.AddModelError("", "Resim Seçiniz");
                }
                else
                {
                    var filmName = await _filmService.FindByFilmAsync(model);
                    if (filmName == null)
                    {
                        string imageName = null;
                        if (image != null)
                        {
                            string extension = Path.GetExtension(image.FileName);
                            imageName = Guid.NewGuid() + extension;
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", imageName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }
                        }
                        await _filmService.AddAsync(new Film
                        {
                            Name = model.Name,
                            TRDubbing = model.TRDubbing,
                            TRDubbingUrl = model.TRDubbingUrl,
                            TRSubtitle = model.TRSubtitle,
                            TRSubtitleUrl = model.TRSubtitleUrl,
                            Description = model.Description,
                            Director = model.Director,
                            ImagePath = imageName,
                            PostedTime = model.PostedTime,
                            ReleaseDate = model.ReleaseDate,
                            IMDBPoint = model.IMDBPoint,
                            NumberOfClicks = model.NumberOfClicks,
                            AppUserId = (int)HttpContext.Session.GetInt32("id")
                        });
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Böyle bir isimli film mevcut");
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> AssignCategory(int id)
        {
            TempData["Active"] = "Film";
            TempData["FilmId"] = id;

            var categories = await _categoryService.GetAllAsync();
            var filmCategories = await _filmService.GetCategoriesAsync(id);

            List<AssignCategoryListDto> list = new List<AssignCategoryListDto>();
            foreach (var category in categories)
            {
                AssignCategoryListDto model = new AssignCategoryListDto();
                model.Id = category.Id;
                model.Name = category.Name;
                foreach (var item in filmCategories)
                {
                    if (category.Name == item.Name)
                    {
                        model.Exists = true; break;
                    }
                    else
                    {
                        model.Exists = false;
                    }
                }
                //model.Exists = filmCategories.Contains(category);
                list.Add(model);
            }

            return View(list);
            //return View(_mapper.Map<List<AssignCategoryList>>(await _categoryService.GetAllAsync()));
        }
        [HttpPost]
        public async Task<IActionResult> AssignCategory(List<AssignCategoryListDto> model)
        {
            TempData["Active"] = "Film";
            int id = (int)TempData["FilmId"];
            foreach (var item in model)
            {
                if (item.Exists)
                {
                    CategoryFilmDto categoryFilmDto = new();
                    categoryFilmDto.FilmId = id;
                    categoryFilmDto.CategoryId = item.Id;
                    await _filmService.AddToCategoryAsync(categoryFilmDto);
                }
                else
                {
                    CategoryFilmDto categoryFilmDto = new();
                    categoryFilmDto.FilmId = id;
                    categoryFilmDto.CategoryId = item.Id;
                    await _filmService.RemoveFromCategoryAsync(categoryFilmDto);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFilm(int id)
        {
            TempData["Active"] = "Film";
            var film = await _filmService.FindByIdAsync(id);
            System.IO.File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/img/" + film.ImagePath);
            await _filmService.RemoveAsync(new Film
            {
                Id = id
            });

            return Json(null);
        }

        public async Task<IActionResult> UpdateFilm(int id)
        {
            TempData["Active"] = "Film";
            var film = await _filmService.FindByIdAsync(id);
            TempData["Image"] = film.ImagePath;
            return View(_mapper.Map<FilmUpdateDto>(film));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFilm(FilmUpdateDto model, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                TempData["Active"] = "Film";
                if (image == null)
                {
                    var defaultImage = TempData["Image"];
                    await _filmService.UpdateAsync(new Film
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description,
                        TRDubbing = model.TRDubbing,
                        TRDubbingUrl = model.TRDubbingUrl,
                        TRSubtitle = model.TRSubtitle,
                        TRSubtitleUrl = model.TRSubtitleUrl,
                        Director = model.Director,
                        ReleaseDate = model.ReleaseDate,
                        PostedTime = model.PostedTime,
                        IMDBPoint = model.IMDBPoint,
                        ImagePath = (string)defaultImage,
                        AppUserId = (int)HttpContext.Session.GetInt32("id")
                    });
                    return RedirectToAction("Index");
                }
                else
                {
                    string imageName = null;
                    if (image != null)
                    {
                        System.IO.File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/img/" + (string)TempData["Image"]);

                        string extension = Path.GetExtension(image.FileName);
                        imageName = Guid.NewGuid() + extension;
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", imageName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                    }
                    await _filmService.UpdateAsync(new Film
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description,
                        TRDubbing = model.TRDubbing,
                        TRDubbingUrl = model.TRDubbingUrl,
                        TRSubtitle = model.TRSubtitle,
                        TRSubtitleUrl = model.TRSubtitleUrl,
                        Director = model.Director,
                        ReleaseDate = model.ReleaseDate,
                        PostedTime = model.PostedTime,
                        IMDBPoint = model.IMDBPoint,
                        ImagePath = imageName,
                        AppUserId = (int)HttpContext.Session.GetInt32("id")
                    });
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public IActionResult Slider()
        {
            TempData["Active"] = "Film";
            string sliderText = Directory.GetCurrentDirectory() + "/wwwroot/SliderText/slider.txt";
            if (System.IO.File.Exists(sliderText))
            {
                StreamReader sr = new StreamReader(sliderText);
                string satir = null;
                List<string> filmName = new();
                List<string> IMDBPoint = new();
                List<string> language = new();
                //List<string> ImagePath = new();
                while ((satir = sr.ReadLine()) != null)
                {
                    if (satir != null || satir != "")
                    {
                        filmName.Add(satir.Split('*')[0].ToString());
                        IMDBPoint.Add(satir.Split('*')[1].ToString());
                        language.Add(satir.Split('*')[2].ToString());
                    }
                    //ImagePath.Add(satir.Split(' ')[3].ToString());
                }
                ViewBag.FilmName = filmName;
                ViewBag.IMDBPoint = IMDBPoint;
                ViewBag.Language = language;
                //ViewBag.ImagePath = ImagePath;
                sr.Close();
            }
            return View();
        }

        public IActionResult AddSliderFilm()
        {
            TempData["Active"] = "Film";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSliderFilm(FilmAddDto model, IFormFile image)
        {
            TempData["Active"] = "Film";
            var resultFilm = await _filmService.FindByFilmAsync(model);
            if (resultFilm != null)
            {
                string imageName = null;
                if (image != null)
                {
                    string extension = Path.GetExtension(image.FileName);
                    imageName = Guid.NewGuid() + extension;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Slider/", imageName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }

                string sliderText = Directory.GetCurrentDirectory() + "/wwwroot/SliderText/slider.txt";
                //FileStream fs = new FileStream(sliderText, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(sliderText, true);
                string language = "";
                if (model.TRDubbing == true && model.TRSubtitle == true)
                {
                    language = "Türkçe Dublaj / Altyazılı";
                }
                else if (model.TRDubbing == true)
                {
                    language = "Türkçe Dublaj";
                }
                else
                {
                    language = "Türkçe Altyazılı";
                }
                sw.WriteLine(model.Name + "*" + model.IMDBPoint + "*" + language + "*" + imageName);
                sw.Flush();
                sw.Close();
                //fs.Close();
                return RedirectToAction("Slider");
            }
            else
            {
                ModelState.AddModelError("", "Film İsimleri Uyuşmuyor");
            }
            return View("AddSliderFilm", model);
        }

        public IActionResult DeleteSliderFilm(string name)
        {
            string sliderText = Directory.GetCurrentDirectory() + "/wwwroot/SliderText/slider.txt";
            string[] file = System.IO.File.ReadAllLines(sliderText);
            FileStream fs = new FileStream(sliderText, FileMode.Truncate);
            fs.Close();
            StreamWriter sw = new StreamWriter(sliderText, true);
            for (int i = 0; i < file.Count(); i++)
            {
                if (name != file[i].Split("*")[0])
                    sw.WriteLine(file[i]);
                else
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/img/Slider/" + file[i].Split("*")[3]);
                }
            }
            sw.Flush();
            sw.Close();
            return RedirectToAction("Slider");
        }
    }
}
