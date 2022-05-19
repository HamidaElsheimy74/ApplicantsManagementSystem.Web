using ApplicantsManagementSystem.BAL.Interfaces.ApplicantInterfaces;
using ApplicantsManagementSystem.BAL.Models;
using ApplicantsManagementSystem.BAL.Models.HelperModels;
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
	public class GetByIdApplicantService : IGetByIdApplicantService
	{
		private readonly IRepository<Applicant> repository;
		public GetByIdApplicantService(IRepository<Applicant> repository)
		{
			this.repository = repository;
		}

		/// <summary>
		/// Get applicant by its Id
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public ApplicantResponseModel GetById(int Id)
		{
			try
			{
				if (Id > 0)
				{
					var applicant = repository.GetById(x => x.Id == Id);
					if (applicant != null)
					{
						return new ApplicantResponseModel()
						{
							HasError = false,
							Data = applicant
						};
					}
					else
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.itemNotExist)
						};
					}
				}
				else
				{
					return new ApplicantResponseModel()
					{
						HasError = true,
						ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidId)
					};
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
