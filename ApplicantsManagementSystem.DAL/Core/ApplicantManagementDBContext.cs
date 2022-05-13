using ApplicantsManagementSystem.DAL.Interfaces;
using ApplicantsManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.DAL.Core
{
	public class ApplicantManagementDBContext : DbContext, IApplicantManagementDBContext
	{
		public ApplicantManagementDBContext(DbContextOptions<ApplicantManagementDBContext> options)
			:base(options)
		{

		}
		public DbSet<Applicant> Applicants { get ; set; }

		
	}
}
