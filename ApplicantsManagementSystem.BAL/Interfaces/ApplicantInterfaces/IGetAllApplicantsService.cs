using ApplicantsManagementSystem.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.Interfaces.ApplicantInterfaces
{
	public interface IGetAllApplicantsService
	{
		ApplicantResponseModel GetAll();
	}
}
