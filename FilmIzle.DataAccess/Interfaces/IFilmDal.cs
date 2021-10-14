using FilmIzle.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.DataAccess.Interfaces
{
    public interface IFilmDal : IGenericDal<Film>
    {
        Task<List<Category>> GetCategoriesAsync(int filmId);

        //kategoriye göre film sayısı
        Task<List<Category>> GetNumberOfFilmsByCategoryAsync();

        //kategoriye göre filmler
        Task<Tuple<List<Film>, int>> GetFilmByCategoryAsync(string name, int page);

        //Filmin kategorilerini getirir
        Task<List<Category>> GetCategoriesOfFilmAsync(string name);

        //filmin tıklanma sayısını güncelleme
        Task UpdateClickFilm(string name);
        //en çok yorum yapılan filmlar
        Task<Tuple<List<Film>,int>> GetFilmMostCommentedAsync(int page);
    }
}
