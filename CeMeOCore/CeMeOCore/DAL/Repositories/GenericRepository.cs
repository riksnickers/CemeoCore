using CeMeOCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace CeMeOCore.DAL.Repositories
{
    /// <summary>
    /// This is the generic repository. All functions are generic and this class can be inherited to more specific repositories.
    /// This class is slightly modified from the ASP.NET tutorials.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// The internal context
        /// </summary>
        internal CeMeoContext context;
        /// <summary>
        /// The local Dataset
        /// </summary>
        internal DbSet<TEntity> dbSet;

        /// <summary>
        /// Constructor that sets the context. The initialization should be done by the Unit of work
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(CeMeoContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Get request
        /// </summary>
        /// <param name="filter">Use linq to create a filter</param>
        /// <param name="orderBy">use linq to order</param>
        /// <param name="includeProperties">include some properties</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Get a TEntity object by ID
        /// </summary>
        /// <param name="id">the id</param>
        /// <returns></returns>
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Insert a TEntity object
        /// </summary>
        /// <param name="entity">The entity to add</param>
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Delete an entity by ID
        /// </summary>
        /// <param name="id">The ID of the entity to delete</param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Delete an entity by an entity
        /// </summary>
        /// <param name="entityToDelete">The entity to delete</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Update the specific entity (will check for changed fields, not whole objects)
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            var entry = context.Entry(entityToUpdate);

            entry.CurrentValues.SetValues(entityToUpdate);

        }
    }
}