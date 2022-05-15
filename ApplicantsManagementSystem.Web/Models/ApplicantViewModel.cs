using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.Web.Models
{
	public class ApplicantViewModel
	{
		public int? Id { set; get; }
		public string Name { set; get; }
		public string FamilyName { set; get; }
		public string Address { set; get; }
		public string CountryOfOrigin { set; get; }
		public string EmailAddress { set; get; }
		public int Age { set; get; }
		public bool? Hired { set; get; }
	}
}
