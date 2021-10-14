using FilmIzle.DTO.DTOs.AppUserDtos;
using FilmIzle.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.Business.Interfaces
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> CheckUserAsync(AppUserSigninDto appUserSigninDto);
    }
}
