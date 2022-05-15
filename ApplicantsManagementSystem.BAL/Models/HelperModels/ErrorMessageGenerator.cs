using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantsManagementSystem.BAL.Models.HelperModels
{
	public class ErrorMessageGenerator
	{
		private static Dictionary<Enum, string> Errors = new Dictionary<Enum, string>
		{
			{
				ErrorMessageGeneratorEnum.internalServerError,
				"An error occured while processing your request : "
			},
			{
				ErrorMessageGeneratorEnum.InvalidName,
				"Invalid name, it must be at least 5 characters."
			},
			{
				ErrorMessageGeneratorEnum.EmptyName,
				"The name is empty or null."
			},
			{
				ErrorMessageGeneratorEnum.InvalidFamilyName,
				"Invalid family name, it must be at least 5 characters."
			},
			{
				ErrorMessageGeneratorEnum.EmptyFamilyName,
				"The family name is empty or null."
			},
			{
				ErrorMessageGeneratorEnum.InvalidEmail,
				"Invalid family name, it must be like h@h.com."
			},
			{
				ErrorMessageGeneratorEnum.EmptyEmail,
				"The Email is empty or null."
			},
			{
				ErrorMessageGeneratorEnum.InvalidAddress,
				"Invalid address, it must be at least 10 characters."
			},
			{
				ErrorMessageGeneratorEnum.EmptyAddress,
				"The Address is empty or null."
			},
			{
				ErrorMessageGeneratorEnum.InvalidCountryOfOrigin,
				"Invalid country of origin."
			},
			{
				ErrorMessageGeneratorEnum.EmptyCountryOfOrigin,
				"The country of origin is empty or null."
			},
			{
				ErrorMessageGeneratorEnum.InvalidAge,
				"Age must be between 20 and 60."
			},
			{
				ErrorMessageGeneratorEnum.CheckHired,
				"You have to choose wither the applicant is hired or not."
			},
			{
				ErrorMessageGeneratorEnum.InvalidModel,
				"Model is not valida, please check you data."
			},
			{
				ErrorMessageGeneratorEnum.itemNotExist,
				"This applicant is not exist."
			},
			{
				ErrorMessageGeneratorEnum.InvalidId,
				"Invalid Id, it must be provided and greater than 0."
			}
		};

		public static string GenerateErrorMessage(ErrorMessageGeneratorEnum errorMessageGeneratorEnum)
		{
			return Errors[errorMessageGeneratorEnum];
		}
	}
}
