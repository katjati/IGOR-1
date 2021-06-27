using Igor.Library.Abstract;
using Igor.Library.Global;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DbContext context;
        public virtual DbSet<TEntity> _dbSet { get; set; }

        public Repository(DbContext context)
        {
            this.context = context;
            this._dbSet = context.Set<TEntity>();
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string include = "", bool tracking = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (tracking == false) query = query.AsNoTracking();

            return GetQuery(where: where, orderBy: orderBy, include: include, tracking: tracking).ToList();
        }
        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string include = "", bool tracking = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (tracking == false) query = query.AsNoTracking();

            return await GetQuery(where: where, orderBy: orderBy, include: include, tracking: tracking).ToListAsync();
        }

        /// <inheritdoc />
        public virtual IQueryable<TEntity> GetLocalQuery(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>> include = null)
        {

            IQueryable<TEntity> query = _dbSet.Local.AsQueryable();

            if (query == null) return null;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (include != null)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        /// <inheritdoc />
        public bool AnyOrLocal(Expression<Func<TEntity, bool>> where = null)
        {
            return GetFirstOrLocal(where: where) != null;
        }
        /// <inheritdoc />
        public async Task<bool> AnyOrLocalAsync(Expression<Func<TEntity, bool>> where = null)
        {
            return await GetFirstOrLocalAsync(where: where) != null;
        }
        /// <inheritdoc />
        public bool Any(Func<TEntity, bool> where = null)
        {
            if (where == null)
            {
                return _dbSet.Any();
            }
            else
            {
                return _dbSet.Any(where);
            }
        }
        /// <inheritdoc />
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where = null)
        {
            if (where == null)
            {
                return await _dbSet.AnyAsync();
            }
            else
            {
                return await _dbSet.AnyAsync(where);
            }
        }


        //Include as lambda
        /// <inheritdoc />
        public IEnumerable<TEntity> GetOrLocal(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>> include = null, bool tracking = true)
        {
            IEnumerable<TEntity> localResults = GetLocalQuery(where: where, orderBy: orderBy, include: include).ToList();
            if (localResults.Any()) return localResults;
            else
            {
                IEnumerable<TEntity> dbResult = GetQueryIncluding(where: where, orderBy: orderBy, include: include, tracking: tracking).ToList();
                if (dbResult.Any())
                {
                    return dbResult;
                }
                else return localResults;
            }
        }
        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetOrLocalAsync(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>> include = null, bool tracking = true)
        {
            IEnumerable<TEntity> localResults = GetLocalQuery(where: where, orderBy: orderBy, include: include).ToList();
            if (localResults.Any()) return localResults;
            else
            {
                IEnumerable<TEntity> dbResult = await GetQueryIncluding(where: where, orderBy: orderBy, include: include, tracking: tracking).ToListAsync();
                if (dbResult.Any())
                {
                    return dbResult;
                }
                else return localResults;
            }
        }
        /// <inheritdoc />
        public TEntity GetFirstOrLocal(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>> include = null, bool tracking = true)
        {
            TEntity localResults = GetLocalQuery(where: where, orderBy: orderBy, include: include).FirstOrDefault();
            if (localResults != null) return localResults;
            else
            {
                TEntity dbResult = GetQueryIncluding(where: where, orderBy: orderBy, include: include, tracking: tracking).FirstOrDefault();
                return dbResult ?? null;
            }
        }
        /// <inheritdoc />
        public async Task<TEntity> GetFirstOrLocalAsync(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>> include = null, bool tracking = true)
        {
            TEntity localResults = GetLocalQuery(where: where, orderBy: orderBy, include: include).FirstOrDefault();
            if (localResults != null) return localResults;
            else
            {
                TEntity dbResult = await GetQueryIncluding(where: where, orderBy: orderBy, include: include, tracking: tracking).FirstOrDefaultAsync();
                return dbResult ?? null;
            }
        }


        /// <inheritdoc />
        public virtual IQueryable<TEntity> GetQuery(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string include = "", bool tracking = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (tracking == false) query = query.AsNoTracking();

            if (where != null)
            {
                query = query.Where(where);
            }

            foreach (var includeProperty in include.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        /// <inheritdoc />
        public virtual IQueryable<TEntity> GetQueryIncluding(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>>[] include = null, bool tracking = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (tracking == false) query = query.AsNoTracking();

            if (where != null)
            {
                query = query.Where(where);
            }

            if (!include.IsNullOrEmpty())
            {
                foreach (Expression<Func<TEntity, object>> inc in include)
                {
                    if (inc != null) query = query.Include(inc);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
        /// <inheritdoc />
        public virtual IQueryable<TEntity> GetQueryIncluding(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>> include = null, bool tracking = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (tracking == false) query = query.AsNoTracking();

            if (where != null)
            {
                query = query.Where(where);
            }

            if (include != null)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }


        /// <inheritdoc />
        public virtual TEntity GetByID(object id, bool changeDetection)
        {
            bool currChangeDetection = context.Configuration.AutoDetectChangesEnabled;
            if (!changeDetection) context.Configuration.AutoDetectChangesEnabled = false;
            TEntity result = _dbSet.Find(id);
            if (!changeDetection) context.Configuration.AutoDetectChangesEnabled = currChangeDetection;
            return result;
        }
        /// <inheritdoc />
        public virtual async Task<TEntity> GetByIDAsync(object id, bool changeDetection)
        {
            bool currChangeDetection = context.Configuration.AutoDetectChangesEnabled;
            if (!changeDetection) context.Configuration.AutoDetectChangesEnabled = false;
            TEntity result = await _dbSet.FindAsync(id);
            if (!changeDetection) context.Configuration.AutoDetectChangesEnabled = currChangeDetection;
            return result;
        }

        /// <inheritdoc />
        public virtual IList<TEntity> GetByID(IList<object> ids, bool changeDetection)
        {
            return new List<TEntity>();
        }
        /// <inheritdoc />
        public virtual void Insert(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;
            //dbSet.Add(entity);
        }
        /// <inheritdoc />
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }
        /// <inheritdoc />
        public virtual void Delete(IList<TEntity> entities)
        {
            if (entities == null || (entities != null && !entities.Any())) return;
            for (int i = entities.ToList().Count() - 1; i >= 0; i--)
            {
                Delete(entities.ElementAt(i));
            }
        }
        /// <inheritdoc />
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }
        /// <inheritdoc />
        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        /// <inheritdoc />
        public void Clear()
        {
            if (_dbSet.Local.Any())
            {
                context.Set<TEntity>().Local.Clear();
                /*foreach (TEntity entity in dbSet.Local)
                {
                    
                }*/
            }
            if (_dbSet.Any())
            {
                foreach (TEntity entity in _dbSet)
                {
                    context.Entry(entity).State = EntityState.Deleted;
                }
            }
        }

        /// <inheritdoc />
        public bool Exists(TEntity entity)
        {
            if (_dbSet.Local.Contains(entity)) return true;
            else
            {
                if (context.Entry(entity) != null) return true;
                else return false;
            }
        }
        /// <inheritdoc />
        public bool IsModified(TEntity entity)
        {
            if (context.Entry(entity) != null)
            {
                if (context.Entry(entity).State == EntityState.Modified || context.Entry(entity).State == EntityState.Added) return true;
            }
            return false;
        }
        /// <inheritdoc />
        public bool IsDetached(TEntity entity)
        {
            if (context.Entry(entity) != null)
            {
                if (context.Entry(entity).State == EntityState.Detached) return true;
            }
            return false;
        }
        /// <inheritdoc />
        public void SetToChanged(TEntity entity)
        {
            if (context.Entry(entity) != null)
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    Attach(entity);
                }
                else context.Entry(entity).State = EntityState.Modified;
            }
        }
        /// <inheritdoc />
        public void Attach(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            //context.Entry(entity).State = EntityState.Modified;
        }
        /// <inheritdoc />
        public virtual void Commit()
        {
            context.SaveChanges();
        }
        /// <inheritdoc />
        public void Refresh(TEntity entity)
        {
            context.Entry(entity).Reload();
        }
    }
}
