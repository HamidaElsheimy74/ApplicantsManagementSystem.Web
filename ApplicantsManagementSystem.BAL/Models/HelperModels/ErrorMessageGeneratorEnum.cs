using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.Models.HelperModels
{
	public enum ErrorMessageGeneratorEnum
	{
		internalServerError=1,
		InvalidName=2,
		EmptyName=3,
		InvalidFamilyName=4,
		EmptyFamilyName=5,
		InvalidEmail =6,
		EmptyEmail=7,
		InvalidAddress=8,
		EmptyAddress=9,
		InvalidCountryOfOrigin=10,
		EmptyCountryOfOrigin=11,
		InvalidAge=12,
		CheckHired=13,
		InvalidModel=14,
		itemNotExist=15,
		InvalidId=16,
	}
}
