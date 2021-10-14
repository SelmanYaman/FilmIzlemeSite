using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIzle.Web.ViewComponents
{
    public class Slider : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string sliderText = Directory.GetCurrentDirectory() + "/wwwroot/SliderText/slider.txt";
            if (File.Exists(sliderText))
            {
                StreamReader sr = new StreamReader(sliderText);
                string satir = null;
                List<string> filmName = new();
                List<string> IMDBPoint = new();
                List<string> language = new();
                List<string> ImagePath = new();
                while ((satir = sr.ReadLine()) != null)
                {
                    if (satir != null || satir != "")
                    {
                        filmName.Add(satir.Split('*')[0].ToString());
                        IMDBPoint.Add(satir.Split('*')[1].ToString());
                        language.Add(satir.Split('*')[2].ToString());
                        ImagePath.Add(satir.Split('*')[3].ToString());
                    }
                }
                ViewBag.FilmName = filmName;
                ViewBag.IMDBPoint = IMDBPoint;
                ViewBag.Language = language;
                ViewBag.ImagePath = ImagePath;
                sr.Close();
            }
            return View();

        }
    }
}
