using ApplicantsManagementSystem.BAL.HelperMethods.Interfaces;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.Services.ApplicantServices
{
	public class UpdateApplicantService : IUpdateApplicantService
	{
		private readonly IRepository<Applicant> repository;
		IHelperMethod helperMethod;
		public UpdateApplicantService(IRepository<Applicant> repository,IHelperMethod helperMethod)
		{
			this.repository = repository;
			this.helperMethod = helperMethod;
		}

		/// <summary>
		/// updates apllicant
		/// </summary>
		/// <param name="model"></param>
		public ApplicantResponseModel Update(UpdateApplicantRequestModel model)
		{
			try
			{
				if (model.Id>0)
				{

					if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name))
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.EmptyName)
						};
					}
					if (model.Name.Length < 5)
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidName)
						};
					}
					if (string.IsNullOrEmpty(model.FamilyName) || string.IsNullOrWhiteSpace(model.FamilyName))
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.EmptyFamilyName)
						};
					}
					if (model.FamilyName.Length < 5)
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidFamilyName)
						};
					}
					if (string.IsNullOrEmpty(model.EmailAddress) || string.IsNullOrWhiteSpace(model.EmailAddress))
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.EmptyEmail)
						};
					}
					if (!Regex.IsMatch(model.EmailAddress, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidEmail)
						};
					}

					if (string.IsNullOrEmpty(model.Address) || string.IsNullOrWhiteSpace(model.Address))
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.EmptyAddress)
						};
					}

					if (model.Address.Length < 10)
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidAddress)
						};
					}
					if (string.IsNullOrEmpty(model.CountryOfOrigin) || string.IsNullOrWhiteSpace(model.CountryOfOrigin))
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.EmptyCountryOfOrigin)
						};
					}

					//calling countries API
					var isCountryOriginal =helperMethod.IsCountryOriginal(model.CountryOfOrigin).Result;
					if (!isCountryOriginal)
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidCountryOfOrigin)
						};
					}
					if (model.Age <= 20 || model.Age > 60)
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidAge)
						};
					}

					if (model.Hired != null && !model.Hired.HasValue)
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.CheckHired)
						};
					}

					var applicant = repository.GetById(x => x.Id == model.Id);
					if (applicant != null)
					{
						applicant.Name = model.Name;
						applicant.FamilyName = model.FamilyName;
						applicant.EmailAddress = model.EmailAddress;
						applicant.Address = model.Address;
						applicant.CountryOfOrigin = model.CountryOfOrigin;
						applicant.Age = model.Age;
						applicant.UpdateDate = DateTime.UtcNow;
						applicant.Hired = model.Hired;

						var isUpdated = repository.Update(applicant);
						return new ApplicantResponseModel()
						{
							HasError = false,
							Data = isUpdated
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
