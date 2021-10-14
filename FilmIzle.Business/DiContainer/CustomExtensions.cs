using FilmIzle.Business.Concrete;
using FilmIzle.Business.Interfaces;
using FilmIzle.Business.ValidationRules.FluentValidation;
using FilmIzle.DataAccess.Concrete.EntityFrameworkCore.Context;
using FilmIzle.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using FilmIzle.DataAccess.Interfaces;
using FilmIzle.DTO.DTOs.CategoryDtos;
using FilmIzle.DTO.DTOs.FilmDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.Business.DiContainer
{
    public static class CustomExtensions
    {
        public static void AddContainerWithDependencies(this IServiceCollection services,IConfiguration configuration)
        {
            //filmcontext construction çalışması için şart :)
            services.AddDbContext<FilmContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("db1"));
            });

            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<IFilmService, FilmManager>();
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            services.AddScoped<IAppUserDal, EfAppUserRepository>();
            services.AddScoped<ICategoryDal, EfCategoryRepository>();
            services.AddScoped<ICommentDal, EfCommentRepository>();
            services.AddScoped<IFilmDal, EfFilmRepository>();
            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));
        }

        public static void AddCustomValidator(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CategoryAddDto>, CategoryAddValidator>();
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateValidator>();

            services.AddTransient<IValidator<FilmAddDto>, FilmAddValidator>();
            services.AddTransient<IValidator<FilmUpdateDto>, FilmUpdateValidator>();
        }
    }
}
