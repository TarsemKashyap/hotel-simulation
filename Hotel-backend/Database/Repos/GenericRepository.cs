using Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public interface IRepository<TEntity> where TEntity : class
{
    void Delete(object id);
    void Delete(TEntity entityToDelete);
    Task<TEntity> FindAsync(object id);
    Task<IEnumerable<TEntity>> FindAsync();
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, object>> include = null);
    Task Insert(TEntity entity);
    void Update(TEntity entityToUpdate);
}

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly HotelDbContext context;
    private DbSet<TEntity> dbSet;
    string errorMessage = string.Empty;
    public Repository(HotelDbContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> FindAsync(object id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task Insert(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }

    public virtual void Delete(object id)
    {
        TEntity entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
    public async Task<IEnumerable<TEntity>> FindAsync()
    {
        return await dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await dbSet.Where(expression).AsNoTracking().ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> FindAsync(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           Expression<Func<TEntity, object>> include = null)
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (include != null)
        {
            query = query.Include(include);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

}
