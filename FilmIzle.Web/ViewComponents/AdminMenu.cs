using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIzle.Web.ViewComponents
{
    public class AdminMenu:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
