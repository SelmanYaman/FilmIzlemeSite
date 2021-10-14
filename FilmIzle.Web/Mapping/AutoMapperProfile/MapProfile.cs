using AutoMapper;
using FilmIzle.DTO.DTOs.AppUserDtos;
using FilmIzle.DTO.DTOs.CategoryDtos;
using FilmIzle.DTO.DTOs.CommentDtos;
using FilmIzle.DTO.DTOs.FilmDtos;
using FilmIzle.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmIzle.Web.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region AppUser-AppUserDTO
            CreateMap<AppUserSigninDto, AppUser>();
            CreateMap<AppUser, AppUserSigninDto>();
            CreateMap<AppUserUpdateDto, AppUser>();
            CreateMap<AppUser, AppUserUpdateDto>();
            #endregion


            #region Category-CategoryDto
            CreateMap<CategoryAddDto, Category>();
            CreateMap<Category, CategoryAddDto>();
            CreateMap<CategoryListDto, Category>();
            CreateMap<Category, CategoryListDto>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>();
            CreateMap<CategoryFilmCountListDto, Category>();
            CreateMap<Category, CategoryFilmCountListDto>();
            #endregion


            #region Commnet-CommentDto
            CreateMap<CommentListDto, Comment>();
            CreateMap<Comment, CommentListDto>();
            CreateMap<CommentAddDto, Comment>();
            CreateMap<Comment, CommentAddDto>();
            #endregion


            #region Film-FilmDto
            CreateMap<FilmAddDto, Film>();
            CreateMap<Film, FilmAddDto>();
            CreateMap<FilmListAllDto, Film>();
            CreateMap<Film, FilmListAllDto>();
            CreateMap<Category, AssignCategoryListDto>();
            CreateMap<AssignCategoryListDto, Category>();
            CreateMap<FilmUpdateDto, Film>();
            CreateMap<Film, FilmUpdateDto>();


            CreateMap<FilmListDto, Film>();
            CreateMap<Film, FilmListDto>();
            CreateMap<FilmWatchDto, Film>();
            CreateMap<Film, FilmWatchDto>();
            #endregion
        }
    }
}
