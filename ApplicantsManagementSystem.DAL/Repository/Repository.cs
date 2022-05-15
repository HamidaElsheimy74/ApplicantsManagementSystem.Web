using ApplicantsManagementSystem.DAL.Core;
using ApplicantsManagementSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.DAL.Repository
{
	public class Repository<T> : IRepository<T> where T :class
	{
		private readonly ApplicantManagementDBContext context;

		public Repository(ApplicantManagementDBContext context)
		{
			this.context = context;
		}

		public bool Add(T obj)
		{
			try
			{
				bool isAdded = false;
				if(obj!=null)
				{
					var item=context.Add<T>(obj);
					if (item != null)
					{
						context.SaveChanges();
						isAdded = true;
					}
					else
						isAdded = false;
				}
				return isAdded;
			}
			catch(Exception)
			{
				throw;
			}
		}

		public bool Update(T obj)
		{
			try
			{
				bool isUpdated = false;
				if(obj!=null)
				{
					var updatedObj=context.Update(obj);
					if(updatedObj!=null)
					{
						context.SaveChanges();
						isUpdated = true;
					}
					else
					{
						isUpdated = false;
					}
				}
				return isUpdated;
			}
			catch (Exception)
			{
				throw;
			}
		}
		

		public IEnumerable<T> GetAll()
		{
			try
			{
				var list=context.Set<T>().ToList();
				if(list.Count>0 && list!=null)
				{
					return list;
				}
				else
				{
					return null;
				}
			}
			catch(Exception)
			{
				throw;
			}
		}


		public T GetById(Expression<Func<T, bool>> where)
		{
			try
			{

				var item =context.Set<T>().Where(where).FirstOrDefault();
				if (item != null)
				{
					return item;
				}
				else
					return null;
				
			}
			catch (Exception)
			{
				throw;
			}
		}
		public bool Delete(Expression<Func<T, bool>> where)
		{
			try
			{
				bool isDeleted = false;
				var item = context.Set<T>().Where(where).FirstOrDefault();
				if (item != null)
				{
					var removedObj = context.Remove(item);
					if (removedObj != null)
					{
						context.SaveChanges();
						isDeleted = true;
					}
				}

				return isDeleted;
				
			}
			catch (Exception)
			{
				throw;
			}
		}

	}
}
