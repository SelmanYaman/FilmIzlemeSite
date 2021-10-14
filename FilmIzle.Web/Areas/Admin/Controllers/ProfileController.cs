using AutoMapper;
using FilmIzle.Business.Interfaces;
using FilmIzle.DTO.DTOs.AppUserDtos;
using FilmIzle.Entities.Concrete;
using FilmIzle.Web.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIzle.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [UserFilter]
    public class ProfileController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        public ProfileController(IAppUserService appUserService,IMapper mapper)
        {
            _mapper = mapper;
            _appUserService = appUserService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = "Profile";
            return View(_mapper.Map<AppUserUpdateDto>(await _appUserService.FindByIdAsync((int)HttpContext.Session.GetInt32("id"))));
        }
        [HttpPost]
        public async Task<IActionResult> Index(AppUserUpdateDto model)
        {
            TempData["Active"] = "Profile";
            await _appUserService.UpdateAsync(new AppUser
            {
                Id = model.Id,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Name = model.Name,
                SurName = model.SurName
            });
            return RedirectToAction("Index");
        }
    }
}
