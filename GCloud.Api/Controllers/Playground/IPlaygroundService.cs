using System.Net.Http;
using JetBrains.Annotations;

namespace KP.Api.Controllers.Playground
{
	public interface IPlaygroundService
	{
		[NotNull]
		PlaygroundMethodDesc[] GetStaffObjectsMethods([NotNull] HttpRequestMessage request);
	}
}