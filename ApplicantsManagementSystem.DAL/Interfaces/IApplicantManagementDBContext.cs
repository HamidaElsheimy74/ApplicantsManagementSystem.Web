using ApplicantsManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.DAL.Interfaces
{
	public interface IApplicantManagementDBContext
	{
		 public DbSet<Applicant> Applicants { set; get; }

	}
}
