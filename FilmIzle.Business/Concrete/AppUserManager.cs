using FilmIzle.Business.Interfaces;
using FilmIzle.DataAccess.Interfaces;
using FilmIzle.DTO.DTOs.AppUserDtos;
using FilmIzle.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser>,IAppUserService
    {
        private readonly IGenericDal<AppUser> _genericDal;
        public AppUserManager(IGenericDal<AppUser> genericDal) : base(genericDal)
        {
            _genericDal = genericDal;
        }

        public async Task<AppUser> CheckUserAsync(AppUserSigninDto appUserSigninDto)
        {
            return await _genericDal.GetAsync(I => I.UserName == appUserSigninDto.UserName && I.Password == appUserSigninDto.Password);
        }
    }
}
