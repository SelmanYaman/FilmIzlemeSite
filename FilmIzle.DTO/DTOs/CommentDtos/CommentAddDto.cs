using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.DTO.DTOs.CommentDtos
{
    public class CommentAddDto
    {
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; }
        public int? ParentCommentId { get; set; }
        public int FilmId { get; set; }
    }
}
