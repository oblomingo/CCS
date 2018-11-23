using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCS.Web.Extensions
{
	public static class ApiExtensions
	{
		public static ActionResult<Result<T>> ToResult<T>(this T obj)
		{
			var resultWrapper = new Result<T> { OkResult = obj };

			var actionResult = new ActionResult<Result<T>>(resultWrapper);

			return actionResult;
		}

		public static ActionResult<Result<T>> ToNotNullResult<T>(this T obj, object id = null, string messageNotFound = null)
		{
			if (obj == null)
			{
				if (messageNotFound == null)
				{
					messageNotFound = id == null ?
						$"Item of type {typeof(T)} not found" :
						$"Item of type {typeof(T)} not found using id: {id}";
				}
				else
				{
					messageNotFound = string.Format(messageNotFound, id);
				}

				return new ObjectResult(new Result<T> { Error = messageNotFound })
				{
					DeclaredType = typeof(Result<T>),
					StatusCode = StatusCodes.Status404NotFound
				};
			}

			return obj.ToResult();
		}
	}
}
