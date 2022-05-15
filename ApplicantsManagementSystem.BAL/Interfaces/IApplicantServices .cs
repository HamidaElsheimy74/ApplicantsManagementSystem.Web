using ApplicantsManagementSystem.BAL.Models;
using ApplicantsManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.Interfaces
{
	public interface IApplicantServices
	{
		ApplicantResponseModel Add(ApplicantModel model);
		ApplicantResponseModel Update(ApplicantModel model);
		ApplicantResponseModel Delete(int Id);
		ApplicantResponseModel GetAll();
		ApplicantResponseModel GetById(int Id);
	}
}
