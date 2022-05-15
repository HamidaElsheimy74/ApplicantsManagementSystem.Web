using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.Models
{
	public class ApplicantModel
	{
		public int? Id { set; get; }
		[Required]
		public string Name { set; get; }
		[Required]
		public string FamilyName { set; get; }
		[Required]
		public string Address { set; get; }
		[Required]
		public string CountryOfOrigin { set; get; }
		[Required]
		public string EmailAddress { set; get; }
		[Required]
		public int Age { set; get; }

		public bool? Hired { set; get; }
	}
}
