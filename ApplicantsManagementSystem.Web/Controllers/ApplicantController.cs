using ApplicantsManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.Web.Controllers
{
	public class ApplicantController : Controller
	{
		private readonly IConfiguration config;
		public ApplicantController(IConfiguration config)
		{
			this.config = config;
		}

		/// <summary>
		/// gets the list of applicants
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			try
			{
				List<ApplicantViewModel> applicants = new List<ApplicantViewModel>();
				var apiUrl = config.GetSection("GetApplicantsApi")?.Value;
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(apiUrl);
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					var response = client.GetAsync(apiUrl).Result;
					if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
					{
						response.EnsureSuccessStatusCode();
						 applicants =response.Content.ReadAsAsync<List<ApplicantViewModel>>().Result; 
						return View( applicants);
					}
					else
					{
						ViewBag.errorMessage= response.Content.ReadAsAsync<string>().Result;
						return View("Error");
					}
				} ;

			
			}
			catch (Exception)
			{
				return RedirectToAction("Error");
			}
		}

		/// <summary>
		/// opens create applicant form
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Create()
		{
			try
			{
				return View();
			}
			catch(Exception)
			{
				return RedirectToAction("Error");
			}
		}

		/// <summary>
		/// Insert new applicant to the system
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult Create(ApplicantViewModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var apiUrl = config.GetSection("AddApplicantApi")?.Value;
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(apiUrl);
						client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
						var response = client.PostAsJsonAsync(apiUrl, model).Result;
						if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
						{
							return	RedirectToAction("Index");
						}
						else
						{
							ViewBag.errorMessage = response.Content.ReadAsAsync<string>().Result;
							return View("Error");
						}
					};
					
				}
				else
				{
					ViewBag.errorMessage = "Invalid model, please make sure of inserting the required data.";
					return View("Error");
				}
			
			}
			catch (Exception)
			{
				return RedirectToAction("Error");
			}
		}

		/// <summary>
		/// Get applicant data to be updated
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Edit(int Id)
		{
			try
			{
				
					if (Id >0)
					{
					var apiUrl = config.GetSection("GetApplicantByIdApi")?.Value+$"?Id={Id.ToString()}";
						using (var client = new HttpClient())
						{
							client.BaseAddress = new Uri(apiUrl);
							client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
							var response = client.GetAsync(apiUrl).Result;
							if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
							{
							var applicant = response.Content.ReadAsAsync<ApplicantViewModel>().Result;
								return View(applicant);
							}
							else
							{
								ViewBag.errorMessage = response.Content.ReadAsAsync<string>().Result;
								return View("Error");
							}
						};
						
					}
					else
					{
						ViewBag.errorMessage = "Applicant Id isn't correct.";
						return RedirectToAction("Error");
					}
				
			}
			catch (Exception)
			{
				return RedirectToAction("Error");
			}
		}

		/// <summary>
		/// Edit applicant data
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult Edit(ApplicantViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var apiUrl = config.GetSection("UpdateApplicantApi")?.Value;
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(apiUrl);
						client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
						var response = client.PutAsJsonAsync(apiUrl, model).Result;
						if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
						{
							return RedirectToAction("Index");
						}
						else
						{
							ViewBag.errorMessage = response.Content.ReadAsAsync<string>().Result;
							return View("Error");
						}
					};

				}
				else
				{
					ViewBag.errorMessage = "Invalid model, please make sure of inserting the required data.";
					return RedirectToAction("Error");
				}

			}
			catch (Exception)
			{
				return RedirectToAction("Error");
			}
		}

		/// <summary>
		/// Get applicant details
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Details(int Id)
		{
			try
			{

				if (Id >= 0)
				{
					var apiUrl = config.GetSection("GetApplicantByIdApi")?.Value + $"?Id={Id.ToString()}";
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(apiUrl);
						client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
						var response = client.GetAsync(apiUrl).Result;
						if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
						{
							var applicant = response.Content.ReadAsAsync<ApplicantViewModel>().Result;
							return View(applicant);
						}
						else
						{
							ViewBag.errorMessage = response.Content.ReadAsAsync<string>().Result;
							return View("Error");
						}
					};

				}
				else
				{
					ViewBag.errorMessage = "Applicant Id isn't correct.";
					return RedirectToAction("Error");
				}

			}
			catch (Exception)
			{
				return RedirectToAction("Error");
			}
		}

		/// <summary>
		/// Delete applicant from system
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public IActionResult Delete(int Id)
		{
			try
			{

				if (Id >= 0)
				{
					var apiUrl = config.GetSection("DeleteApplicantApi")?.Value + $"?Id={Id.ToString()}";
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(apiUrl);
						client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
						var response = client.DeleteAsync(apiUrl).Result;
						if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
						{
							return RedirectToAction("Index");
						}
						else
						{
							ViewBag.errorMessage = response.Content.ReadAsAsync<string>().Result;
							return View("Error");
						}
					};

				}
				else
				{
					ViewBag.errorMessage = "Applicant Id isn't correct.";
					return RedirectToAction("Error");
				}

			}
			catch (Exception)
			{
				return RedirectToAction("Error");
			}
		}

		[Route("Error")]
		public IActionResult Error()
		{
			return View("Error");
		}
	}
}
