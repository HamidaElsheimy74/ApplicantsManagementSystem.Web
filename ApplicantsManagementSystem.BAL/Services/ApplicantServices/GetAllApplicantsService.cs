using ApplicantsManagementSystem.BAL.Interfaces.ApplicantInterfaces;
using ApplicantsManagementSystem.BAL.Models;
using ApplicantsManagementSystem.DAL.Interfaces;
using ApplicantsManagementSystem.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.Services.ApplicantServices
{
	public class GetAllApplicantsService : IGetAllApplicantsService
	{
		private readonly IRepository<Applicant> repository;

		public GetAllApplicantsService(IRepository<Applicant> repository)
		{
			this.repository = repository;
		}
		/// <summary>
		/// Get all applicants from db
		/// </summary>
		/// <returns></returns>
		public ApplicantResponseModel GetAll()
		{
			try
			{
				var applicantList = repository.GetAll().ToList();
				return new ApplicantResponseModel()
				{
					HasError = false,
					Data = applicantList
				};
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
