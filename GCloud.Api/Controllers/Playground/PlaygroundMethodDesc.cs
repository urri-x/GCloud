namespace KP.Api.Controllers.Playground
{
	public class PlaygroundMethodDesc
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string[] Types { get; set; }
		public string UriTemplate { get; set; }
		public PlaygroundParameterDesc[] Parameters { get; set; }
		public bool IsContent { get; set; }
	}
}