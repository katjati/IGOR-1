using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Abstract
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> _dbSet { get; set; }

        /// <summary>
        /// Get list of entities from context based on predicate, order.
        /// Can include entities with name as string.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <param name="orderBy">Order predicate</param>
        /// <param name="include">Included entity</param>
        /// <param name="tracking">Determines whether to let EF track changes in the entities.</param>
        /// <returns>List of entities.</returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string include = "", bool tracking = true);
        /// <summary>
        /// Get list of entities from context based on predicate, order.
        /// Can include entities with name as string.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <param name="orderBy">Order predicate</param>
        /// <param name="include">Included entity</param>
        /// <param name="tracking">Determines whether to let EF track changes in the entities.</param>
        /// <returns>List of entities.</returns>
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string include = "", bool tracking = true);

        /// <summary>
        /// Get list of entities from local cache based on predicate, order as a query (not yet retrieved from DB).
        /// Can include entities with name as string.
        /// NOTE: you must call tolist or count or first etc. to actually make a call to DB.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetLocalQuery(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, object>> include = null);
        /// <summary>
        /// Check if entity based on predicate exists in the context.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Any(Func<TEntity, bool> where = null);
        /// <summary>
        /// Check if entity based on predicate exists in the context.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where = null);
        /// <summary>
        /// Check if entity based on predicate exists in local cache.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool AnyOrLocal(Expression<Func<TEntity, bool>> where = null);
        /// <summary>
        /// Check if entity based on predicate exists in local cache.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<bool> AnyOrLocalAsync(Expression<Func<TEntity, bool>> where = null);
        /// <summary>
        /// Get list of entities from context OR from local cache based on predicate, order.
        /// Can include entities with name as lambda.
        /// NOTE: it does not mix entities from local cache and DB. It's either one or another.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <param name="orderBy">Order predicate</param>
        /// <param name="include">Included entity</param>
        /// <param name="tracking">Determines whether to let EF track changes in the entities.</param>
        /// <returns>List of entities.</returns>
        IEnumerable<TEntity> GetOrLocal(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, object>> include = null, bool tracking = true);
        /// <summary>
        /// Get list of entities from context OR from local cache based on predicate, order in asynchronous manner.
        /// Can include entities with name as lambda.
        /// NOTE: it does not mix entities from local cache and DB. It's either one or another.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <param name="orderBy">Order predicate</param>
        /// <param name="include">Included entity</param>
        /// <param name="tracking">Determines whether to let EF track changes in the entities.</param>
        /// <returns>List of entities.</returns>
        Task<IEnumerable<TEntity>> GetOrLocalAsync(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, object>> include = null, bool tracking = true);
        /// <summary>
        /// Get first entity from context based on predicate, order.
        /// Can include entities with name as lambda.
        /// NOTE: it does not mix entities from local cache and DB. It's either one or another.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <param name="orderBy">Order predicate</param>
        /// <param name="include">Included entity</param>
        /// <param name="tracking">Determines whether to let EF track changes in the entities.</param>
        /// <returns>First matched entity.</returns>
        TEntity GetFirstOrLocal(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, object>> include = null, bool tracking = true);
        /// <summary>
        /// Get first entity from context based on predicate, order.
        /// Can include entities with name as lambda.
        /// NOTE: it does not mix entities from local cache and DB. It's either one or another.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <param name="orderBy">Order predicate</param>
        /// <param name="include">Included entity</param>
        /// <param name="tracking">Determines whether to let EF track changes in the entities.</param>
        /// <returns>First matched entity.</returns>
        Task<TEntity> GetFirstOrLocalAsync(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, object>> include = null, bool tracking = true);
        /// <summary>
        /// Get list of entities from context based on predicate, order as a query.
        /// Can include entities with name as string.
        /// NOTE: it does not mix entities from local cache and DB. It's either one or another.
        /// NOTE: you must call tolist or count or first etc. to actually make a call to DB.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <param name="orderBy">Order predicate</param>
        /// <param name="include">Included entity</param>
        /// <param name="tracking">Determines whether to let EF track changes in the entities.</param>
        /// <returns>Query to get List of entities.</returns>
        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string include = "", bool tracking = true);
        /// <summary>
        /// Get list of entities from context based on predicate, order as a query.
        /// Can include entities with name as array of lambdas.
        /// NOTE: you must call tolist or count or first etc. to actually make a call to DB.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <param name="orderBy">Order predicate</param>
        /// <param name="include">Included entity</param>
        /// <param name="tracking">Determines whether to let EF track changes in the entities.</param>
        /// <returns>Query to get List of entities.</returns>
        IQueryable<TEntity> GetQueryIncluding(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, object>>[] include = null, bool tracking = true);
        /// <summary>
        /// Get list of entities from context based on predicate, order as a query.
        /// Can include entities with name as lambda.
        /// NOTE: you must call tolist or count or first etc. to actually make a call to DB.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <param name="orderBy">Order predicate</param>
        /// <param name="include">Included entity</param>
        /// <param name="tracking">Determines whether to let EF track changes in the entities.</param>
        /// <returns>Query to get List of entities.</returns>
        IQueryable<TEntity> GetQueryIncluding(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, object>> include = null, bool tracking = true);


        /// <summary>
        /// Get the entity with a given primary key/id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changeDetection">Determines whether EF change detection is enabled. Set to true if you intend to update items.</param>
        /// <returns></returns>
        TEntity GetByID(object id, bool changeDetection);
        /// <summary>
        /// Get the entity with a given primary key/id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changeDetection"></param>
        /// <returns></returns>
        Task<TEntity> GetByIDAsync(object id, bool changeDetection);
        /// <summary>
        /// Insert the entity to context as new or attach it if it was detached before.
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);
        /// <summary>
        /// Delete the entity with a given primary key/id from the context.
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
        /// <summary>
        /// Delete the list of entities from the context.
        /// </summary>
        /// <param name="entities"></param>
        void Delete(IList<TEntity> entities);
        /// <summary>
        /// Delete the entity from the context.
        /// </summary>
        void Delete(TEntity entityToDelete);
        /// <summary>
        /// Make the entity modified in the context.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        void Update(TEntity entityToUpdate);
        /// <summary>
        /// Clear the context from all entities of this repository.
        /// </summary>
        void Clear();
        /// <summary>
        /// Check if entity exists in current context.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Exists(TEntity entity);
        /// <summary>
        /// Check if entity is marked as changed.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsModified(TEntity entity);
        /// <summary>
        /// Check if entity is detached from current context.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsDetached(TEntity entity);
        /// <summary>
        /// Make the entity changed so it can be saved.
        /// </summary>
        /// <param name="entity"></param>
        void SetToChanged(TEntity entity);
        /// <summary>
        /// Attaches entity to current context if it was not there before.
        /// </summary>
        /// <param name="entity"></param>
        void Attach(TEntity entity);
        /// <summary>
        /// Saves changes to entities in the context (also in other repositories).
        /// </summary>
        void Commit();
        /// <summary>
        /// Reloads the entity by calling DB.
        /// </summary>
        /// <param name="entity"></param>
        void Refresh(TEntity entity);
    }
}
