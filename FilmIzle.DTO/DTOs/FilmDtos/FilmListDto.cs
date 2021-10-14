using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.DTO.DTOs.FilmDtos
{
    public class FilmListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool TRDubbing { get; set; }
        public bool TRSubtitle { get; set; }
        public decimal IMDBPoint { get; set; }
        public string ImagePath { get; set; }
    }
}
