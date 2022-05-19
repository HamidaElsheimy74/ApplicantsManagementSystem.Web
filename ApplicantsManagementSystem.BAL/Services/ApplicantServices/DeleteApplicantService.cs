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
	public class DeleteApplicantService : IDeleteApplicantService
	{
		private readonly IRepository<Applicant> repository;
	
		public DeleteApplicantService(IRepository<Applicant> repository)
		{
			this.repository = repository;
		}

		/// <summary>
		/// Deletes applicant from db
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public ApplicantResponseModel Delete(int Id)
		{
			try
			{
				if (Id > 0)
				{
					var result = repository.Delete(x => x.Id == Id);
					if (result)
					{
						return new ApplicantResponseModel()
						{
							HasError = false,
							Data = result
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
						Data = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidId)
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
