using FilmIzle.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.Entities.Concrete
{
    public class AppUser : ITable
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public List<Film> Films { get; set; }
    }
}
