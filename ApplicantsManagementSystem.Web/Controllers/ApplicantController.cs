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
						return View();
					}
				} ;

			
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
