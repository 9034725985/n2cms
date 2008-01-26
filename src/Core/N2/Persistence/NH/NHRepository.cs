#region license

// Copyright (c) 2005 - 2007 Ayende Rahien (ayende@ayende.com)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of Ayende Rahien nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using NHibernate;
using NHibernate.Expression;

namespace N2.Persistence.NH
{
	public class NHRepository<TKey, TEntity> : INHRepository<TKey, TEntity>
	{
		#region Private Fields

		private ISessionProvider sessionProvider;

		#endregion

		#region Constructor

		/// <summary>Creates a new instance of the NHRepository.</summary>
		public NHRepository(ISessionProvider sessionProvider)
		{
			this.sessionProvider = sessionProvider;
			Debug.WriteLine("NHRepository: ctor");
		}

		#endregion

		#region Properties 

		public ISessionProvider SessionProvider
		{
			get { return sessionProvider; }
			set { sessionProvider = value; }
		}

		#endregion

		#region Methods

		public TEntity Get(TKey id)
		{
			return sessionProvider.GetOpenedSession().Get<TEntity>(id);
		}

		public T Get<T>(TKey id)
		{
			return sessionProvider.GetOpenedSession().Get<T>(id);
		}

		public TEntity Load(TKey id)
		{
			return sessionProvider.GetOpenedSession().Load<TEntity>(id);
		}

		public virtual void Delete(TEntity entity)
		{
			sessionProvider.GetOpenedSession().Delete(entity);
		}

		public void Save(TEntity entity)
		{
			sessionProvider.GetOpenedSession().Save(entity);
		}

		public void Update(TEntity entity)
		{
			sessionProvider.GetOpenedSession().Update(entity);
		}

		public void SaveOrUpdate(TEntity entity)
		{
			sessionProvider.GetOpenedSession().SaveOrUpdate(entity);
		}

		public ICollection<TEntity> FindAll(Order order, params ICriterion[] criteria)
		{
			ICriteria crit = RepositoryHelper<TEntity>.CreateCriteriaFromArray(sessionProvider.GetOpenedSession(), criteria);
			crit.AddOrder(order);
			return crit.List<TEntity>();
		}

		public ICollection<TEntity> FindAll(Order[] orders, params ICriterion[] criteria)
		{
			ICriteria crit = RepositoryHelper<TEntity>.CreateCriteriaFromArray(sessionProvider.GetOpenedSession(), criteria);
			foreach (Order order in orders)
			{
				crit.AddOrder(order);
			}
			return crit.List<TEntity>();
		}

		public ICollection<TEntity> FindAll(params ICriterion[] criteria)
		{
			ICriteria crit = RepositoryHelper<TEntity>.CreateCriteriaFromArray(sessionProvider.GetOpenedSession(), criteria);
			return crit.List<TEntity>();
		}

		public ICollection<TEntity> FindAll(int firstResult, int numberOfResults, params ICriterion[] criteria)
		{
			ICriteria crit = RepositoryHelper<TEntity>.CreateCriteriaFromArray(sessionProvider.GetOpenedSession(), criteria);
			crit.SetFirstResult(firstResult)
				.SetMaxResults(numberOfResults);
			return crit.List<TEntity>();
		}

		public ICollection<TEntity> FindAll(
			int firstResult, int numberOfResults, Order selectionOrder, params ICriterion[] criteria)
		{
			ICriteria crit = RepositoryHelper<TEntity>.CreateCriteriaFromArray(sessionProvider.GetOpenedSession(), criteria);
			crit.SetFirstResult(firstResult)
				.SetMaxResults(numberOfResults);
			crit.AddOrder(selectionOrder);
			return crit.List<TEntity>();
		}

		public ICollection<TEntity> FindAll(
			int firstResult, int numberOfResults, Order[] selectionOrder, params ICriterion[] criteria)
		{
			ICriteria crit = RepositoryHelper<TEntity>.CreateCriteriaFromArray(sessionProvider.GetOpenedSession(), criteria);
			crit.SetFirstResult(firstResult)
				.SetMaxResults(numberOfResults);
			foreach (Order order in selectionOrder)
			{
				crit.AddOrder(order);
			}
			return crit.List<TEntity>();
		}

		public ICollection<TEntity> FindAll(string namedQuery, params Parameter[] parameters)
		{
			IQuery query = RepositoryHelper<TEntity>.CreateQuery(sessionProvider.GetOpenedSession(), namedQuery, parameters);
			return query.List<TEntity>();
		}

		public ICollection<TEntity> FindAll(
			int firstResult, int numberOfResults, string namedQuery, params Parameter[] parameters)
		{
			IQuery query = RepositoryHelper<TEntity>.CreateQuery(sessionProvider.GetOpenedSession(), namedQuery, parameters);
			query.SetFirstResult(firstResult)
				.SetMaxResults(numberOfResults);
			return query.List<TEntity>();
		}

		public TEntity FindOne(params ICriterion[] criteria)
		{
			ICriteria crit = RepositoryHelper<TEntity>.CreateCriteriaFromArray(sessionProvider.GetOpenedSession(), criteria);
			return (TEntity) crit.UniqueResult();
		}

		public TEntity FindOne(string namedQuery, params Parameter[] parameters)
		{
			IQuery query = RepositoryHelper<TEntity>.CreateQuery(sessionProvider.GetOpenedSession(), namedQuery, parameters);
			return (TEntity) query.UniqueResult();
		}

		public ICollection<TEntity> FindAll(DetachedCriteria criteria, params Order[] orders)
		{
			ICriteria executableCriteria =
				RepositoryHelper<TEntity>.GetExecutableCriteria(sessionProvider.GetOpenedSession(), criteria, orders);
			return executableCriteria.List<TEntity>();
		}

		public ICollection<TEntity> FindAll(DetachedCriteria criteria, int firstResult, int maxResults, params Order[] orders)
		{
			ICriteria executableCriteria =
				RepositoryHelper<TEntity>.GetExecutableCriteria(sessionProvider.GetOpenedSession(), criteria, orders);
			executableCriteria.SetFirstResult(firstResult);
			executableCriteria.SetMaxResults(maxResults);
			return executableCriteria.List<TEntity>();
		}

		public TEntity FindOne(DetachedCriteria criteria)
		{
			ICriteria executableCriteria =
				RepositoryHelper<TEntity>.GetExecutableCriteria(sessionProvider.GetOpenedSession(), criteria, null);
			return (TEntity) executableCriteria.UniqueResult();
		}

		public TEntity FindFirst(DetachedCriteria criteria, params Order[] orders)
		{
			ICriteria executableCriteria =
				RepositoryHelper<TEntity>.GetExecutableCriteria(sessionProvider.GetOpenedSession(), criteria, orders);
			executableCriteria.SetFirstResult(0);
			executableCriteria.SetMaxResults(1);
			return (TEntity) executableCriteria.UniqueResult();
		}

		/// <summary>
		/// Find the first entity of type
		/// </summary>
		/// <param name="orders">Optional orderring</param>
		/// <returns>The entity or null</returns>
		public TEntity FindFirst(params Order[] orders)
		{
			return FindFirst(null, orders);
		}

		/// <summary>
		/// Check if any instance matches the criteria.
		/// </summary>
		/// <returns><c>true</c> if an instance is found; otherwise <c>false</c>.</returns>
		public bool Exists(DetachedCriteria criteria)
		{
			return 0 != Count(criteria);
		}

		/// <summary>
		/// Check if any instance of the type exists
		/// </summary>
		/// <returns><c>true</c> if an instance is found; otherwise <c>false</c>.</returns>
		public bool Exists()
		{
			return Exists(null);
		}

		/// <summary>
		/// Counts the number of instances matching the criteria.
		/// </summary>
		/// <param name="criteria"></param>
		/// <returns></returns>
		public long Count(DetachedCriteria criteria)
		{
			ICriteria crit = RepositoryHelper<TEntity>.GetExecutableCriteria(sessionProvider.GetOpenedSession(), criteria, null);
			crit.SetProjection(Projections.RowCount());
			object countMayBe_Int32_Or_Int64_DependingOnDatabase = crit.UniqueResult();
			return Convert.ToInt64(countMayBe_Int32_Or_Int64_DependingOnDatabase);
		}

		/// <summary>
		/// Counts the overall number of instances.
		/// </summary>
		/// <returns></returns>
		public long Count()
		{
			return Count(null);
		}

		public void Dispose()
		{
			sessionProvider.Dispose();
		}

		public void Flush()
		{
			sessionProvider.GetOpenedSession().Flush();
		}

		public ITransaction BeginTransaction()
		{
			return new NHTransaction(sessionProvider);
		}

		#endregion
	}
}