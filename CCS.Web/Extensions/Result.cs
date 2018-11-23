using Newtonsoft.Json;

namespace CCS.Web.Extensions
{
	public class Result<T>
	{
		[JsonProperty("result")]
		public T OkResult { get; set; }

		[JsonProperty("error")]
		public string Error { get; set; }
	}
}
