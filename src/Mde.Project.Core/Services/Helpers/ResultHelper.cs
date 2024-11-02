using Mde.Project.Core.Services.Models;

namespace Pri.Pe1.Hsp.Core.Services.Helpers
{
	public static class ResultHelper
	{
		public static BaseResultModel CreateErrorResult(string error)
		{
			return new BaseResultModel
			{
				IsSuccess = false,
				Errors = new List<string>() { error }
			};
		}

		public static ResultModel<T> CreateErrorResult<T>(string error)
		{
			return new ResultModel<T>
			{
				IsSuccess = false,
				Errors = new List<string>() { error }
			};
		}

	}
}
