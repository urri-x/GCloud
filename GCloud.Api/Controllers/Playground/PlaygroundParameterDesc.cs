namespace KP.Api.Controllers.Playground
{
	public class PlaygroundParameterDesc
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool FromUriTemplate { get; set; }
		public bool FromBody { get; set; }
		public bool Optional { get; set; }
		public string[] Values { get; set; }
	}
}