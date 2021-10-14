using FilmIzle.DataAccess.Concrete.EntityFrameworkCore.Context;
using FilmIzle.DataAccess.Interfaces;
using FilmIzle.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfAppUserRepository : EfGenericRepository<AppUser>,IAppUserDal
    {
        public EfAppUserRepository(FilmContext context):base(context)
        {

        }
    }
}
