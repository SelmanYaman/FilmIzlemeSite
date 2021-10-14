using AutoMapper;
using FilmIzle.Business.Interfaces;
using FilmIzle.DTO.DTOs.AppUserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIzle.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        public LoginController(IAppUserService appUserService,IMapper mapper)
        {
            _mapper = mapper;
            _appUserService = appUserService;
        }
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("id").HasValue)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signin(AppUserSigninDto model)
        {
            var user = await _appUserService.CheckUserAsync(model);
            if(user != null)
            {
                HttpContext.Session.SetInt32("id", user.Id);
                HttpContext.Session.SetString("fullName", user.Name + " " + user.SurName);
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            return View("Index", model);
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Login");
        }
    }
}
