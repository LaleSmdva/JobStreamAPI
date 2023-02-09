using JobStream.Core.Interfaces;
using JobStream.DataAccess.Contexts;
using JobStream.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobStream.DataAccess.Repositories.Implementations
{
	public class Repository<T> : IRepository<T> where T : class, IEntity, new()
	{
		private readonly AppDbContext _context;

		public Repository(AppDbContext context)
		{
			_context = context;
		}

		public DbSet<T> _table =>_context.Set<T>();

		public IQueryable<T> GetAll()
		{
			return _table.AsQueryable();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _table.FindAsync(id);
		}

		public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
		{
			return _table.Where(expression);
		}

		public async Task CreateAsync(T entity)
		{
			await _table.AddAsync(entity);
		}

		public void Delete(T entity)
		{
			_table.Remove(entity);
		}

	

		public void Update(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
		}
	

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

	}
}
