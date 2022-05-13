using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.Domain.Models
{
	[Table("Applicant")]
	public class Applicant: Entity
	{
		public string Name { set; get; }
		public string FamilyName { set; get; }
		public string Address { set; get; }
		public string CountryOfOrigin { set; get; }
		public string EmailAdress { set; get; }
		public int Age { set; get; }
		public bool Hired { set; get; }

	}
}
