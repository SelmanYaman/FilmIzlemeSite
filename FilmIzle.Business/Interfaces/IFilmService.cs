using FilmIzle.DTO.DTOs.CategoryDtos;
using FilmIzle.DTO.DTOs.CategoryFilmDtos;
using FilmIzle.DTO.DTOs.FilmDtos;
using FilmIzle.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.Business.Interfaces
{
    public interface IFilmService : IGenericService<Film>
    {
        //böyle bir film var mı?
        Task<Film> FindByFilmAsync(FilmAddDto filmAddDto);
        //id ye göre kategorileri çekme
        Task<List<Category>> GetCategoriesAsync(int filmId);
        Task AddToCategoryAsync(CategoryFilmDto categoryFilmDto);
        Task RemoveFromCategoryAsync(CategoryFilmDto categoryFilmDto);
        //filmleri listele
        Task<List<Film>> GetFilmsSortedDescendingAsync();
        Task<Tuple<List<Film>, int>> GetFilmsSortedDescendingAsync(int page);
        //aranan filmleri listele
        Task<Tuple<List<Film>, int>> GetFilmSearchAsync(string search, int page);
        //kategorilere göre film sayısı
        Task<List<Category>> GetNumberOfFilmsByCategoryAsync();
        //kategoriye göre filmler
        Task<Tuple<List<Film>, int>> GetFilmByCategoryAsync(string name, int page);
        //izlenmek için seçilen film
        Task<Film> GetFilmWatchAsync(string name);
        //Filmin kategorilerini getirir
        Task<List<Category>> GetCategoriesOfFilmAsync(string name);
        //tıklanma sayısı arttırma
        Task UpdateClickFilm(string name);
        //film tıklanma sayısına göre sıralama
        Task<Tuple<List<Film>, int>> GetFilmMostClickedAsync(int page);
        //en çok yorum alanlar
        Task<Tuple<List<Film>, int>> GetFilmMostCommentedAsync(int page);
        //Imdb 7den büyük olanlar
        Task<Tuple<List<Film>, int>> GetFilmImdbPointAsync(int page);
        //2021 filmleri
        Task<Tuple<List<Film>, int>> GetFilmYear2021Async(int page);
        //TR dublaj
        Task<Tuple<List<Film>, int>> GetFilmTRDubbingAsync(int page);
        //TR Altyazı
        Task<Tuple<List<Film>, int>> GetFilmTRSubtitleAsync(int page);
    }
}
