using FilmIzle.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.Entities.Concrete
{
    public class CategoryFilm : ITable
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
    }
}
