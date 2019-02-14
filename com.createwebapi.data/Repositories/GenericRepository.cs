using System;
using System.Collections.Generic;
using com.createwebapi.data.Helper;
using com.createwebapi.model.Repositores;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Util;

namespace com.createwebapi.data.Repositories
{

        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            protected ISession Session => NHibernateHelper.Session;

            public IEnumerable<T> GetAll()
            {
                return Session.Query<T>();
            }

            public T GetById(int id)
            {
                return Session.Get<T>(id);
            }

            public int Create(T entity)
            {
                var id = (int)Session.Save(entity);
                Session.Flush();
                return id;
            }

            public void Create(IList<T> entityList)
            {
                if (entityList != null && !entityList.Any()) return;
                Session.SetBatchSize(entityList.Count);
                foreach (var entity in entityList) CreateNoFlush(entity);
                Session.Flush();
                Session.SetBatchSize(1);
            }

            public void Update(IList<T> entityList)
            {
                if (entityList != null && !entityList.Any()) return;
                Session.SetBatchSize(entityList.Count);
                foreach (var entity in entityList) UpdateNoFlush(entity);
                Session.Flush();
                Session.SetBatchSize(1);
            }

            public void Update(T entity)
            {
                Session.Update(entity);
            }

            public void Delete(int id)
            {
                Session.Delete(Session.Load<T>(id));
            }

            /// <summary>
            ///     Dissasociates the entity with the ORM so that changes made to it are not automatically
            ///     saved to the database.  More precisely, this removes the entity from <see cref="ISession" />'s cache.
            ///     More details may be found at http://www.hibernate.org/hib_docs/nhibernate/html_single/#performance-sessioncache.
            /// </summary>
            public virtual void Evict(T entity)
            {
                Session.Evict(entity);
            }

            public virtual void Evict(IList<T> entities)
            {
                foreach (var entity in entities)
                    Session.Evict(entity);
            }

            private void CreateNoFlush(T entity)
            {
                Session.Save(entity);
            }

            private void UpdateNoFlush(T entity)
            {
                Session.Update(entity);
            }

            protected ICriteria CreateCriteria(Type type, string alias)
            {
                return Session.CreateCriteria(type, alias);
            }

            protected ICriteria CreateCriteria(Type type)
            {
                return Session.CreateCriteria(type);
            }

            protected ICriteria CreateCriteria()
            {
                return Session.CreateCriteria<T>();
            }

            /// <summary>
            ///     Loads all the entities that match the criteria
            ///     by order
            /// </summary>
            /// <param name="criteria">the criteria to look for</param>
            /// <returns>All the entities that match the criteria</returns>
            public virtual IList<T2> FindAll<T2>(DetachedCriteria criteria)
            {
                var crit = ExecutableCriteria<T2>(criteria);
                return crit.List<T2>();
            }

            public ICriteria ExecutableCriteria<T2>(DetachedCriteria criteria)
            {
                var crit = criteria != null
                    ? GetExecutableCriteriaFromClone(criteria)
                    : Session.CreateCriteria(typeof(T2));

                return crit;
            }

            private ICriteria GetExecutableCriteriaFromClone(DetachedCriteria criteria)
            {
                var clone = SerializationHelper.Serialize(criteria);
                var crit = (DetachedCriteria)SerializationHelper.Deserialize(clone);
                return crit.GetExecutableCriteria(Session);
            }

            /// <summary>
            ///     Find a single entity based on a criteria.
            ///     Thorws is there is more than one result.
            /// </summary>
            /// <param name="criteria">The criteria to look for</param>
            /// <returns>The entity or null</returns>
            public virtual T FindOne(DetachedCriteria criteria)
            {
                var crit = ExecutableCriteria<T>(criteria);
                return crit.UniqueResult<T>();
            }

            public T1 FindOne<T1>(DetachedCriteria criteria)
            {
                var crit = ExecutableCriteria<T>(criteria);
                return crit.UniqueResult<T1>();
            }
        }
    }