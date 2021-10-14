using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.DTO.DTOs.FilmDtos
{
    public class FilmAddDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool TRDubbing { get; set; }
        public string TRDubbingUrl { get; set; }
        public bool TRSubtitle { get; set; }
        public string TRSubtitleUrl { get; set; }
        public string Director { get; set; }//yönetmen adı
        public string ImagePath { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;
        public DateTime ReleaseDate { get; set; }//gösterime girdiği tarih
        public decimal IMDBPoint { get; set; }
        public int NumberOfClicks { get; set; } = 0;//tıklanma sayısı
        public int AppUserId { get; set; }
    }
}
