using FilmIzle.DataAccess.Concrete.EntityFrameworkCore.Context;
using FilmIzle.DataAccess.Interfaces;
using FilmIzle.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfCommentRepository : EfGenericRepository<Comment>, ICommentDal
    {
        private readonly FilmContext _context;
        public EfCommentRepository(FilmContext context):base(context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int filmId, int? parentId)
        {
            List<Comment> result = new List<Comment>();
            await GetComments(filmId, parentId, result);
            return result;
        }
        private async Task GetComments(int filmId, int? parentId, List<Comment> result)
        {
            var comments = await _context.Comments.Where(I => I.FilmId == filmId && I.ParentCommentId == parentId).OrderByDescending(I => I.PostedTime).ToListAsync();
            if (comments.Count > 0)
            {
                foreach (var comment in comments)
                {
                    if (comment.SubComments == null)
                        comment.SubComments = new List<Comment>();

                    await GetComments(comment.FilmId, comment.Id, comment.SubComments);

                    if (!result.Contains(comment))
                    {
                        result.Add(comment);
                    }
                }
            }
        }
    }
}
