using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.Models
{
	public class UpdateApplicantRequestModel:AddApplicantRequestModel
	{
		[Required]
		public int Id { set; get; }
	}
}
