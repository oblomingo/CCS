using CCS.Repository.Entities;
using CSS.GPIO;

namespace CCS.Web.Services.ControlLogic
{
	public interface IControlLogic
	{
		bool ShouldBeOn();
	}
}
