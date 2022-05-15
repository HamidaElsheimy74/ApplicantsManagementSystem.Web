
using ApplicantsManagementSystem.BAL.Interfaces;
using ApplicantsManagementSystem.BAL.Models;
using ApplicantsManagementSystem.BAL.Models.HelperModels;
using ApplicantsManagementSystem.DAL.Interfaces;
using ApplicantsManagementSystem.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.Services
{
	
	public class ApplicantServices : IApplicantServices
	{
		private readonly IRepository<Applicant> repository;
		private IConfiguration config;
		public ApplicantServices(IRepository<Applicant> repository,IConfiguration configuration)
		{
			this.repository = repository;
			config = configuration;
		}
		/// <summary>
		/// Add new applicant 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public ApplicantResponseModel Add(ApplicantModel model)
		{
			try
			{
				if(string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name))
				{
					return new ApplicantResponseModel()
					{
						HasError = true,
						ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.EmptyName)
					};
				}
				if (model.Name.Length<5)
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
				if (model.FamilyName.Length<5)
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
				if (!Regex.IsMatch( model.EmailAddress, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
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

				if (model.Address.Length<10)
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
				var isCountryOriginal = IsCountryOriginal(model.CountryOfOrigin).Result;
					if (!isCountryOriginal)
					{
						return new ApplicantResponseModel()
						{
							HasError = true,
							ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidCountryOfOrigin)
						};
					}
				if (model.Age<=20 || model.Age>60)
				{
					return new ApplicantResponseModel()
					{
						HasError = true,
						ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidAge)
					};
				}

				if ( model.Hired!=null&&!model.Hired.HasValue)
				{
					return new ApplicantResponseModel()
					{
						HasError = true,
						ErrorMessage = ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.CheckHired)
					};
				}

				var applicant = new Applicant()
				{
					CreationDate=DateTime.UtcNow,
					Name=model.Name,
					FamilyName=model.FamilyName,
					EmailAddress=model.EmailAddress,
					Address=model.Address,
					Age=model.Age,
					Hired=model.Hired,
					CountryOfOrigin=model.CountryOfOrigin

				};
				 var isAdded=repository.Add(applicant);

				return new ApplicantResponseModel()
				{
					HasError = false,
					Data=isAdded
				};
			}
			catch(Exception)
			{
				throw;
			}
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
			catch(Exception)
			{
				throw;
			}
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
			catch(Exception)
			{
				throw;
			}
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
					var applicant =repository.GetById(x=>x.Id==Id);
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
						ErrorMessage =ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidId)
					};
			}
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// updates apllicant
		/// </summary>
		/// <param name="model"></param>
		public ApplicantResponseModel Update(ApplicantModel model)
		{
			try
			{
				if(model.Id!=null)
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
					var isCountryOriginal = IsCountryOriginal(model.CountryOfOrigin).Result;
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
					if(applicant!=null)
					{
						applicant.Name = model.Name;
						applicant.FamilyName = model.FamilyName;
						applicant.EmailAddress = model.EmailAddress;
						applicant.Address = model.Address;
						applicant.CountryOfOrigin = model.CountryOfOrigin;
						applicant.Age = model.Age;
						applicant.UpdateDate = DateTime.UtcNow;

						var isUpdated=repository.Update(applicant);
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
			catch(Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// return true if the country name is exist
		/// </summary>
		/// <param name="countyName"></param>
		/// <returns></returns>
		private async Task< bool> IsCountryOriginal(string countyName)
		{
			try
			{
				bool isOriginal = false;
				using (var client = new HttpClient())
				{
					var url = config.GetSection("CountryOfOriginAPI")?.Value + $"{ countyName}" + config.GetSection("SuffixUri")?.Value;
					client.BaseAddress = new Uri(url);
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					var response = await client.GetAsync(url);
					if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
					{
						isOriginal = true;
					}
					else
						isOriginal = false;
				}

				return isOriginal;
			}
			catch(Exception)
			{
				throw;
			}
		}
	}
}
