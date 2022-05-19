using ApplicantsManagementSystem.BAL.HelperMethods.Concretes;
using ApplicantsManagementSystem.BAL.HelperMethods.Interfaces;
using ApplicantsManagementSystem.BAL.Interfaces.ApplicantInterfaces;
using ApplicantsManagementSystem.BAL.Services.ApplicantServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.DI
{
	public static class DI
	{
		public static void RegisterServices(this IServiceCollection  services)
		{
			services.AddScoped<IAddApplicantService, AddApplicantService>();
			services.AddScoped<IUpdateApplicantService, UpdateApplicantService>();
			services.AddScoped<IDeleteApplicantService, DeleteApplicantService>();
			services.AddScoped<IGetByIdApplicantService, GetByIdApplicantService>();
			services.AddScoped<IGetAllApplicantsService, GetAllApplicantsService>();
			services.AddScoped<IHelperMethod, HelperMethod>();

		}
	}
}
