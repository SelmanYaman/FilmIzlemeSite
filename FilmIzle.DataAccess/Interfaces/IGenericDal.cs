using FilmIzle.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmIzle.DataAccess.Interfaces
{
    public interface IGenericDal<TEntity> where TEntity:class,ITable,new()
    {
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        Task<List<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> keySelector);
        //where getir şuan göre getir. Bu metodum hangi tabloda çalıştırmak istersem orada tanımlıcam
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> FindByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }
}
