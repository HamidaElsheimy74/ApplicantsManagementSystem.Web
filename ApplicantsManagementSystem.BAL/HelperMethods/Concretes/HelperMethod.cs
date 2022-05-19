using ApplicantsManagementSystem.BAL.HelperMethods.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.HelperMethods.Concretes
{
	public class HelperMethod : IHelperMethod
	{
		IConfiguration config;
		public HelperMethod(IConfiguration config)
		{
			this.config = config;
		}

		/// <summary>
		/// return true if the country name is exist
		/// </summary>
		/// <param name="countyName"></param>
		/// <returns></returns>
		public async Task<bool> IsCountryOriginal(string countyName)
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
			catch (Exception)
			{
				throw;
			}
		}
	}
}
