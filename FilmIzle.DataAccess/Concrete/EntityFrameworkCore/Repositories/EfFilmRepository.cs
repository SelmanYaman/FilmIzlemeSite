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
    public class EfFilmRepository : EfGenericRepository<Film>, IFilmDal
    {
        private readonly FilmContext _context;
        public EfFilmRepository(FilmContext context):base(context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategoriesAsync(int filmId)
        {
            return await _context.Categories.Join(_context.CategoryFilms, c => c.Id, cb => cb.CategoryId, (category, categoryFilm) => new
            {
                category,
                categoryFilm
            }).Where(I => I.categoryFilm.FilmId == filmId).Select(I => new Category
            {
                Id = I.category.Id,
                Name = I.category.Name
            }).ToListAsync();
        }

        public async Task<List<Category>> GetNumberOfFilmsByCategoryAsync()
        {
            return await _context.Categories.Join(_context.CategoryFilms, c => c.Id, cf => cf.CategoryId, (category, categoryFilm) => new
            {
                category,
                categoryFilm
            }).Join(_context.Films, cf => cf.categoryFilm.FilmId, f => f.Id, (resultTable, resultFilm) => new
            {
                category = resultTable.category,
                categoryFilm = resultTable.categoryFilm,
                film = resultFilm
            }).GroupBy(I => I.category.Name).Select(I => new Category()
            {
                Name = I.Key,
                Id = I.Count()
            }).OrderByDescending(I => I.Id).ToListAsync();

        }

        public async Task<Tuple<List<Film>,int>> GetFilmByCategoryAsync(string name,int page)
        {
            var resultFilm = await _context.Categories.Join(_context.CategoryFilms, c => c.Id, cf => cf.CategoryId, (category, categoryFilm) => new
            {
                category,
                categoryFilm
            }).Join(_context.Films, cf => cf.categoryFilm.FilmId, f => f.Id, (resultTable, resultFilm) => new
            {
                category = resultTable.category,
                categoryFilm = resultTable.categoryFilm,
                film = resultFilm
            }).OrderByDescending(I => I.film.PostedTime).Where(I => I.category.Name == name).Select(I => new Film()
            {
                Name = I.film.Name,
                TRDubbing = I.film.TRDubbing,
                TRSubtitle = I.film.TRSubtitle,
                ImagePath = I.film.ImagePath,
                Director = I.film.Director,
                ReleaseDate = I.film.ReleaseDate,
                IMDBPoint = I.film.IMDBPoint
            }).ToListAsync();

            var pageCount = (int)Math.Ceiling((double)resultFilm.Count / 20);
            resultFilm = resultFilm.Skip((page - 1) * 20).Take(20).ToList();
            return new Tuple<List<Film>, int>(resultFilm, pageCount);
        }


        public async Task<List<Category>> GetCategoriesOfFilmAsync(string name)
        {
            return await _context.Categories.Join(_context.CategoryFilms, c => c.Id, cf => cf.CategoryId, (category, categoryFilms) => new
            {
                category,
                categoryFilms
            }).Join(_context.Films, cf => cf.categoryFilms.FilmId, f => f.Id, (resultTable, resultFilm) => new
            {
                category = resultTable.category,
                categoryFilm = resultTable.categoryFilms,
                film = resultFilm
            }).Where(I => I.film.Name == name).Select(I => new Category()
            {
                Name = I.category.Name
            }).ToListAsync();
        }

        public async Task UpdateClickFilm(string name)
        {
            var resultFilm = _context.Films.FirstOrDefault(I => I.Name == name);
            resultFilm.NumberOfClicks += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<Tuple<List<Film>,int>> GetFilmMostCommentedAsync(int page)
        {
            var resultFilm = await _context.Films.Join(_context.Comments, f => f.Id, c => c.FilmId, (films, comments) => new
            {
                films,
                comments
            }).GroupBy(I => new { I.films.Id, I.films.Name, I.films.IMDBPoint, I.films.TRDubbing, I.films.TRSubtitle, I.films.ImagePath }).OrderByDescending(I => I.Count()).Select(I => new Film()
            {
                Id = I.Key.Id,
                Name = I.Key.Name,
                IMDBPoint = I.Key.IMDBPoint,
                TRDubbing = I.Key.TRDubbing,
                TRSubtitle = I.Key.TRSubtitle,
                ImagePath = I.Key.ImagePath
            }).ToListAsync();

            var pageCount = (int)Math.Ceiling((double)resultFilm.Count / 20);
            resultFilm = resultFilm.Skip((page - 1) * 20).Take(20).ToList();
            return new Tuple<List<Film>, int>(resultFilm, pageCount);
        }
    }
}
