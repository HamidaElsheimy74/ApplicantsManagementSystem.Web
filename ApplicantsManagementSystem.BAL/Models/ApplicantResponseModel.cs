using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.Models
{
	public class ApplicantResponseModel
	{
		public bool HasError { set; get; }
		public string ErrorMessage { set; get; }
		public object Data { set; get; }

	}
}
