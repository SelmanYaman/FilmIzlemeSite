using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.DTO.DTOs.FilmDtos
{
    public class FilmUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool TRDubbing { get; set; }
        public string TRDubbingUrl { get; set; }
        public bool TRSubtitle { get; set; }
        public string TRSubtitleUrl { get; set; }
        public string Director { get; set; }//yönetmen adı
        public string ImagePath { get; set; }
        public DateTime ReleaseDate { get; set; }//gösterime girdiği tarih
        public decimal IMDBPoint { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
