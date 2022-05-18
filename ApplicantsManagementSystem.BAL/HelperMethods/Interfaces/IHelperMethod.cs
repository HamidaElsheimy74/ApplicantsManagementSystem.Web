using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.HelperMethods.Interfaces
{
	public interface IHelperMethod
	{
		Task<bool> IsCountryOriginal(string countyName);
	}
}
