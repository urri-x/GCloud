using System.Web.Http;

namespace KP.Api.Core.Helpers
{
	public interface IViewResult : IHttpActionResult
	{
		string ViewVirtualPath { get; }
	}
}