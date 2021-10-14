using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.DTO.DTOs.FilmDtos
{
    public class FilmListAllDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }//yönetmen adı
        public DateTime ReleaseDate { get; set; }//gösterime girdiği tarih
        public decimal IMDBPoint { get; set; }
    }
}
