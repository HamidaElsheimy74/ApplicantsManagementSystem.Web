using ApplicantsManagementSystem.BAL.Interfaces;
using ApplicantsManagementSystem.BAL.Interfaces.ApplicantInterfaces;
using ApplicantsManagementSystem.BAL.Models;
using ApplicantsManagementSystem.BAL.Models.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.Api.Controllers
{

	/// <summary>
	/// manages all applicant operations
	/// </summary>
	[Route("api/")]
	[ApiController]
	public class ApplicantManagementController : ControllerBase
	{
		//private readonly IApplicantServices applicantServices;
		IAddApplicantService addApplicantService;
		IUpdateApplicantService updateApplicantService;
		IDeleteApplicantService deleteApplicantService;
		IGetByIdApplicantService getByIdApplicantService;
		IGetAllApplicantsService getAllApplicantsService;
		public ApplicantManagementController(IAddApplicantService addApplicantService,IUpdateApplicantService updateApplicantService,
											IDeleteApplicantService deleteApplicantService,IGetByIdApplicantService getByIdApplicantService,
											IGetAllApplicantsService getAllApplicantsService)
		{
			//this.applicantServices = applicantServices;
			this.addApplicantService = addApplicantService;
			this.updateApplicantService = updateApplicantService;
			this.deleteApplicantService = deleteApplicantService;
			this.getByIdApplicantService = getByIdApplicantService;
			this.getAllApplicantsService = getAllApplicantsService;
		}
		/// <summary>
		/// Add new applicant
		/// </summary>
		/// <param name="model"></param>
		/// <remarks>
		/// 
		///	Request Sample :
		/// 
		///		{
		///			"Name": "hamida",
		///			"FamilyName": "hamida",
		///			"EmailAddress": "ham@ham.ham",
		///			"Address": "nasr city, 5 street",
		///			"CountryOfOrigin": "Egypt",
		///			"Age": 27,
		///			"Hired": true
		///		}
		///		
		/// Response Sample:
		/// 
		/// 
		///	Status Code: 400 BadRequest if name is empty or null
		/// 
		///     {
		///      
		///      "ErrorMessage" : "The name is empty or null."
		///     }
		///     
		/// Status Code: 400 BadRequest if name is less  than 5 letters
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid name, it must be at least 5 characters."
		///     }
		///     
		/// Status Code: 400 BadRequest if family name is empty or null
		/// 
		///     {
		///      
		///      "ErrorMessage" : "The family name is empty or null."
		///     }
		///     
		/// Status Code: 400 BadRequest if family name is smaller than 5 letters
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid family name, it must be at least 5 characters."
		///     }
		///     
		/// Status Code: 400 BadRequest if email is empty or null
		/// 
		///     {
		///      
		///      "ErrorMessage" : "The Email is empty or null."
		///     }
		/// 
		///	Status Code: 400 BadRequest if email is invalid format
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid family name, it must be like h@h.com."
		///     }
		///     
		/// Status Code: 400 BadRequest if Address is empty or null
		/// 
		///     {
		///      
		///      "ErrorMessage" : "The Address is empty or null."
		///     }
		///     
		/// Status Code: 400 BadRequest if Address is less than 10 letters
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid address, it must be at least 10 characters."
		///     }
		///			
		/// Status Code: 400 BadRequest if country of origin is null or  empty
		/// 
		///     {
		///      
		///      "ErrorMessage" : "The country of origin is empty or null."
		///     }
		///     
		/// Status Code: 400 BadRequest if country of origin is invalid
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid country of origin."
		///     }
		///     
		/// Status Code: 400 BadRequest if Age is less than 20 and greater than 60
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Age must be between 20 and 60."
		///     }
		///     
		/// Status Code: 400 BadRequest if country of hired is null or  empty
		/// 
		///     {
		///      
		///      "ErrorMessage" : "You have to choose wither the applicant is hired or not."
		///     }
		///     
		///  Status Code: 400 BadRequest if country of hired is null or  empty
		/// 
		///     {
		///      
		///      "ErrorMessage" : " This applicant is already exist."
		///     }
		///    
		/// 
		/// Status Code: 500 BadRequest if server error occured
		/// 
		///     {
		///      
		///      "ErrorMessage" : "An error occured while processing your request :"
		///     }
		///		
		/// Status Code: 200 ok if success
		/// 
		///     {
		///			true
		///     }
		/// </remarks>
		/// <returns></returns>
		[Route("Applicant/Add")]
		[HttpPost]
		public IActionResult AddAplicant([FromBody] ApplicantModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var response =addApplicantService.Add(model);
					if(response.HasError)
					{
						return StatusCode((int)HttpStatusCode.BadRequest,
						 response.ErrorMessage);
					}
					else
						return StatusCode((int)HttpStatusCode.OK,
								response.Data);
				}
				else
				{
					return StatusCode((int)HttpStatusCode.BadRequest,
						ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidModel));
					
				}

			}
			catch(Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError,
						ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.internalServerError) + ex.Message
	);

			}

		}
		/// <summary>
		/// update  applicant data
		/// </summary>
		/// <param name="model"></param>
		/// <remarks>
		/// 
		///	Request Sample :
		/// 
		///		{
		///			"Id":1,
		///			"Name": "hamida",
		///			"FamilyName": "hamida",
		///			"EmailAddress": "ham@ham.ham",
		///			"Address": "nasr city, 5 street",
		///			"CountryOfOrigin": "Egypt",
		///			"Age": 27,
		///			"Hired": true
		///		}
		///		
		/// Response Sample:
		/// 
		/// 
		///	Status Code: 400 BadRequest if name is empty or null
		/// 
		///     {
		///      
		///      "ErrorMessage" : "The name is empty or null."
		///     }
		///     
		/// Status Code: 400 BadRequest if name is less  than 5 letters
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid name, it must be at least 5 characters."
		///     }
		///     
		/// Status Code: 400 BadRequest if family name is empty or null
		/// 
		///     {
		///      
		///      "ErrorMessage" : "The family name is empty or null."
		///     }
		///     
		/// Status Code: 400 BadRequest if family name is smaller than 5 letters
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid family name, it must be at least 5 characters."
		///     }
		///     
		/// Status Code: 400 BadRequest if email is empty or null
		/// 
		///     {
		///      
		///      "ErrorMessage" : "The Email is empty or null."
		///     }
		/// 
		/// 
		///Status Code: 400 BadRequest if email is invalid format
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid family name, it must be like h@h.com."
		///     }
		///     
		/// Status Code: 400 BadRequest if Address is empty or null
		/// 
		///     {
		///      
		///      "ErrorMessage" : "The Address is empty or null."
		///     }
		///     
		/// Status Code: 400 BadRequest if Address is less than 10 letters
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid address, it must be at least 10 characters."
		///     }
		///			
		/// Status Code: 400 BadRequest if country of origin is null or  empty
		/// 
		///     {
		///      
		///      "ErrorMessage" : "The country of origin is empty or null."
		///     }
		///     
		/// Status Code: 400 BadRequest if country of origin is invalid
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid country of origin."
		///     }
		///     
		/// Status Code: 400 BadRequest if Age is less than 20 and greater than 60
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Age must be between 20 and 60."
		///     }
		///     
		/// Status Code: 400 BadRequest if country of hired is null or  empty
		/// 
		///     {
		///      
		///      "ErrorMessage" : "You have to choose wither the applicant is hired or not."
		///     }
		///     
		/// 
		/// Status Code: 400 BadRequest if Id is not valid
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid Id, it must be provided and greater than 0."
		///     }
		///
		/// 
		/// Status Code: 400 BadRequest if no applicant was found with the given Id
		/// 
		///     {
		///      
		///      "ErrorMessage" : "This applicant is not exist."
		///     }
		///
		/// Status Code: 500 BadRequest if server error occured
		/// 
		///     {
		///      
		///      "ErrorMessage" : "An error occured while processing your request :"
		///     }
		///		
		/// Status Code: 200 ok if success
		/// 
		///     {
		///			true
		///     }
		/// </remarks>
		/// <returns></returns>
		[Route("Applicant/Update")]
		[HttpPut]
		public IActionResult UpdateApplicant([FromBody] ApplicantModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var response = updateApplicantService.Update(model);
					if (response.HasError)
					{
						return StatusCode((int)HttpStatusCode.BadRequest,
						 response.ErrorMessage);
					}
					else
						return StatusCode((int)HttpStatusCode.OK,
							response.Data
							);
				}
				else
				{
					return StatusCode((int)HttpStatusCode.BadRequest,
						ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.InvalidModel));

				}

			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError,
						 ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.internalServerError) + ex.Message);

			}
		}

		/// <summary>
		/// gets all the applicants
		/// </summary>
		/// <remarks>
		/// Response Sample:
		///
		/// Status Code: 500 BadRequest if server error occured
		/// 
		///     {
		///      
		///      "ErrorMessage" : "An error occured while processing your request :"
		///     }
		///		
		/// Status Code: 200 ok if success
		/// 
		///			{
		///				 "Name": "hamida",
		///  			 "FamilyName": "hamida",
		/// 			 "Address": "nasr city city",
		///   			 "CountryOfOrigin": null,
		///  			 "EmailAdress": "ham@ham.ham",
		///				 "Age": 27,
		///				 "Hired": true,
		///   			 "Id": 1,
		///    			 "CreationDate": "2022-05-14T12:15:41.8348553",
		///   			 "UpdateDate": null
		///    		}
		/// 		 
		///     
		/// </remarks>
		/// <returns></returns>
		[Route("Applicant/GetAll")]
		[HttpGet]
		public IActionResult GetAll()
		{
			try
			{
				var response = getAllApplicantsService.GetAll();
				if(response.HasError)
				{
					return StatusCode((int)HttpStatusCode.BadRequest,
						
							 response.ErrorMessage);
				}
				else
				{
					return StatusCode((int)HttpStatusCode.OK,
						response.Data
						
						);
				}
			}
			catch(Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError,
						 ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.internalServerError) + ex.Message);
			}
		}

		/// <summary>
		/// Get applicant by Id
		/// </summary>
		/// <remarks>
		/// 
		/// Request Sample:
		/// 
		///		{
		///				"Id":1
		///		}
		///
		/// Response Sample:
		/// 
		/// Status Code: 400 BadRequest if Id is not valid
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid Id, it must be provided and greater than 0."
		///     }
		///
		/// 
		/// Status Code: 400 BadRequest if no applicant was found with the given Id
		/// 
		///     {
		///      
		///      "ErrorMessage" : "This applicant is not exist."
		///     }
		///
		/// 
		/// Status Code: 500 BadRequest if server error occured
		/// 
		///     {
		///      
		///      "ErrorMessage" : "An error occured while processing your request :"
		///     }
		///		
		/// 
		/// Status Code: 200 ok if success
		/// 
		///			{
		///			     "Name": "hamida",
		///  			 "FamilyName": "hamida",
		/// 			 "Address": "nasr city city",
		///   			 "CountryOfOrigin": null,
		///  			 "EmailAdress": "ham@ham.ham",
		///				 "Age": 27,
		///				 "Hired": true,
		///   			 "Id": 1,
		///    			 "CreationDate": "2022-05-14T12:15:41.8348553",
		///   			 "UpdateDate": null
		///   			 
		///    		}
		///     
		/// 
		/// </remarks>
		/// <param name="Id"></param>
		/// <returns></returns>
		[Route("Applicant/GetById")]
		[HttpGet]
		public IActionResult GetById(int Id)
		{
			try
			{
				var response = getByIdApplicantService.GetById(Id);
				if(response.HasError)
				{
					return StatusCode((int)HttpStatusCode.BadRequest,
						response.ErrorMessage);
				}
				else
				{
					return StatusCode((int)HttpStatusCode.OK,
						response.Data);
				}
			}
			catch(Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError,
						ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.internalServerError) + ex.Message);
			}
		}

		/// <summary>
		/// Delete applicant
		/// </summary>
		/// <remarks>
		/// 
		/// Request Sample:
		/// 
		///		{
		///				"Id":1
		///		}
		///
		/// Response Sample:
		/// 
		/// 
		/// Status Code: 400 BadRequest if no applicant was found with the given Id
		/// 
		///     {
		///      
		///      "ErrorMessage" : "This applicant is not exist."
		///     }
		///
		/// Status Code: 400 BadRequest if Id is not valid
		/// 
		///     {
		///      
		///      "ErrorMessage" : "Invalid Id, it must be provided and greater than 0."
		///     }
		///
		/// 
		/// Status Code: 500 BadRequest if server error occured
		/// 
		///     {
		///      
		///      "ErrorMessage" : "An error occured while processing your request :"
		///     }
		///		
		/// 
		/// Status Code: 200 ok if success
		/// 
		///     {
		///			 true
		///     }
		///     
		/// 
		/// </remarks>
		/// <param name="Id"></param>
		/// <returns></returns>

		[Route("Applicant/Delete")]
		[HttpDelete]
		public IActionResult DeleteApplicant(int Id)
		{
			try
			{
				var response = deleteApplicantService.Delete(Id);
				if (response.HasError)
				{
					return StatusCode((int)HttpStatusCode.BadRequest,
						 response.ErrorMessage);
				}
				else
				{
					return StatusCode((int)HttpStatusCode.OK,
						 response.Data);
				}
			}
			catch(Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError,
						ErrorMessageGenerator.GenerateErrorMessage(ErrorMessageGeneratorEnum.internalServerError) + ex.Message);
			}
		}


	}
}
