using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.DAL.Interfaces
{
	public interface IRepository<T>
	{
		public bool Add(T obj);
		public bool Update(T obj);
		public bool Delete(Expression<Func<T, bool>> where);
		public T GetById(Expression<Func<T, bool>> where);
		public IEnumerable< T>GetAll();


	}
}
