using JetBrains.Annotations;

namespace KP.Api.Core.Helpers
{
	public interface IServerPathProvider
	{
		[CanBeNull]
		string MapPath([NotNull] string virtualPath);
	}
}