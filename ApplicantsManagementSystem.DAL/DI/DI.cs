using ApplicantsManagementSystem.DAL.Interfaces;
using ApplicantsManagementSystem.DAL.Repository;
using ApplicantsManagementSystem.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.DAL.DI
{
	public static class DI
	{
		public static void RegisterDAL(this IServiceCollection services)
		{
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		}
	}
}
