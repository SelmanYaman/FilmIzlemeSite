using FilmIzle.Business.Interfaces;
using FilmIzle.DataAccess.Interfaces;
using FilmIzle.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.Business.Concrete
{
    public class CommentManager : GenericManager<Comment>,ICommentService
    {
        private readonly IGenericDal<Comment> _genericDal;
        private readonly ICommentDal _commentDal;
        public CommentManager(IGenericDal<Comment> genericDal,ICommentDal commentDal) : base(genericDal)
        {
            _commentDal = commentDal;
        }

        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int filmId, int? parentId)
        {
            return await _commentDal.GetAllWithSubCommentsAsync(filmId, parentId);
        }
    }
}
