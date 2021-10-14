using FilmIzle.Business.Interfaces;
using FilmIzle.DataAccess.Interfaces;
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

namespace FilmIzle.Business.Concrete
{
    public class FilmManager : GenericManager<Film>,IFilmService
    {
        private readonly IGenericDal<Film> _genericDal;
        private readonly IGenericDal<CategoryFilm> _categoryFilmService;
        private readonly IFilmDal _filmDal;
        public FilmManager(IGenericDal<Film> genericDal, IGenericDal<CategoryFilm> categoryFilmService,IFilmDal filmDal) :base(genericDal)
        {
            _genericDal = genericDal;
            _filmDal = filmDal;
            _categoryFilmService = categoryFilmService;
        }

        public async Task AddToCategoryAsync(CategoryFilmDto categoryFilmDto)
        {
            var control = await _categoryFilmService.GetAsync(I => I.CategoryId == categoryFilmDto.CategoryId && I.FilmId == categoryFilmDto.FilmId);
            if(control == null)
            {
                await _categoryFilmService.AddAsync(new CategoryFilm
                {
                    CategoryId = categoryFilmDto.CategoryId,
                    FilmId = categoryFilmDto.FilmId
                });
            }
        }

        public async Task RemoveFromCategoryAsync(CategoryFilmDto categoryFilmDto)
        {
            var control = await _categoryFilmService.GetAsync(I => I.CategoryId == categoryFilmDto.CategoryId && I.FilmId == categoryFilmDto.FilmId);
            if(control != null)
            {
                await _categoryFilmService.RemoveAsync(control);
            }
        }
        public async Task<List<Category>> GetCategoriesAsync(int filmId)
        {
            return await _filmDal.GetCategoriesAsync(filmId);
        }

        public async Task<List<Film>> GetFilmsSortedDescendingAsync()
        {
            return await _filmDal.GetAllAsync(I => I.PostedTime);
        }
        public async Task<Tuple<List<Film>, int>> GetFilmsSortedDescendingAsync(int page)
        {
            var resultFilm = await _filmDal.GetAllAsync(I => I.PostedTime);
            var pageCount = (int)Math.Ceiling((double)resultFilm.Count / 20);
            resultFilm = resultFilm.Skip((page - 1) * 20).Take(20).ToList();
            return new Tuple<List<Film>, int>(resultFilm, pageCount);
        }

        public async Task<Tuple<List<Film>,int>> GetFilmSearchAsync(string search, int page)
        {
            var resultFilm = await _filmDal.GetAllAsync(I => I.Name.ToLower().Contains(search.ToLower()), I => I.PostedTime);
            var pageCount = (int)Math.Ceiling((double)resultFilm.Count / 20);
            resultFilm = resultFilm.Skip((page - 1) * 20).Take(20).ToList();
            return new Tuple<List<Film>, int>(resultFilm, pageCount);
        }
        public async Task<List<Category>> GetNumberOfFilmsByCategoryAsync()
        {
            return await _filmDal.GetNumberOfFilmsByCategoryAsync();
        }

        public async Task<Tuple<List<Film>, int>> GetFilmByCategoryAsync(string name, int page)
        {
            return await _filmDal.GetFilmByCategoryAsync(name, page);
        }

        public async Task<Film> FindByFilmAsync(FilmAddDto filmAddDto)
        {
            return await _filmDal.GetAsync(I => I.Name.ToLower() == filmAddDto.Name.ToLower());
        }

        public async Task<Film> GetFilmWatchAsync(string name)
        {
            return await _filmDal.GetAsync(I => I.Name == name);
        }

        public async Task<List<Category>> GetCategoriesOfFilmAsync(string name)
        {
            return await _filmDal.GetCategoriesOfFilmAsync(name);
        }

        public async Task UpdateClickFilm(string name)
        {
            await _filmDal.UpdateClickFilm(name);
        }

        public async Task<Tuple<List<Film>,int>> GetFilmMostClickedAsync(int page)
        {
            var resultFilm = await _filmDal.GetAllAsync(I => I.NumberOfClicks);
            var pageCount = (int)Math.Ceiling((double)resultFilm.Count / 20);
            resultFilm = resultFilm.Skip((page - 1) * 20).Take(20).ToList();
            return new Tuple<List<Film>, int>(resultFilm, pageCount);
        }

        public async Task<Tuple<List<Film>,int>> GetFilmMostCommentedAsync(int page)
        {
            return await _filmDal.GetFilmMostCommentedAsync(page);
        }

        public async Task<Tuple<List<Film>,int>> GetFilmImdbPointAsync(int page)
        {
            var resultFilm = await _filmDal.GetAllAsync(I => I.IMDBPoint >= 7, I => I.IMDBPoint);
            var pageCount = (int)Math.Ceiling((double)resultFilm.Count / 20);
            resultFilm = resultFilm.Skip((page - 1) * 20).Take(20).ToList();
            return new Tuple<List<Film>, int>(resultFilm, pageCount);
        }

        public async Task<Tuple<List<Film>,int>> GetFilmYear2021Async(int page)
        {
            var resultFilm = await _filmDal.GetAllAsync(I => I.ReleaseDate.Year == 2021, I => I.PostedTime);
            var pageCount = (int)Math.Ceiling((double)resultFilm.Count / 20);
            resultFilm = resultFilm.Skip((page - 1) * 20).Take(20).ToList();
            return new Tuple<List<Film>, int>(resultFilm, pageCount);
        }

        public async Task<Tuple<List<Film>, int>> GetFilmTRDubbingAsync(int page)
        {
            var resultFilm = await _filmDal.GetAllAsync(I => I.TRDubbing == true, I => I.PostedTime);
            var pageCount = (int)Math.Ceiling((double)resultFilm.Count / 20);
            resultFilm = resultFilm.Skip((page - 1) * 20).Take(20).ToList();
            return new Tuple<List<Film>, int>(resultFilm, pageCount);
        }

        public async Task<Tuple<List<Film>, int>> GetFilmTRSubtitleAsync(int page)
        {
            var resultFilm = await _filmDal.GetAllAsync(I => I.TRSubtitle == true, I => I.PostedTime);
            var pageCount = (int)Math.Ceiling((double)resultFilm.Count / 20);
            resultFilm = resultFilm.Skip((page - 1) * 20).Take(20).ToList();
            return new Tuple<List<Film>, int>(resultFilm, pageCount);
        }
    }
}
