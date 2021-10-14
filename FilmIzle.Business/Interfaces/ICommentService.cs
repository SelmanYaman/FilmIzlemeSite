using FilmIzle.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.Business.Interfaces
{
    public interface ICommentService : IGenericService<Comment>
    {
        Task<List<Comment>> GetAllWithSubCommentsAsync(int filmId, int? parentId);
    }
}
