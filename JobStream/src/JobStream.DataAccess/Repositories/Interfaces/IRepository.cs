using JobStream.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Repositories.Interfaces
{
	public interface IRepository<T> where T : class,IEntity,new()
	{
		IQueryable<T> GetAll();
		Task<T> GetByIdAsync(int id);
		IQueryable<T> GetByCondition(Expression<Func<T,bool>> expression);
		Task CreateAsync(T entity);
		void Update(T entity);	
		void Delete(T entity);
		Task SaveAsync();
		DbSet<T> _table { get; }

	}
}
