namespace KP.Api.Controllers.Playground
{
	public class PlaygroundBodyAttribute : PlaygroundDescriptionAttributeBase
	{
		public PlaygroundBodyAttribute(string name, string description)
			: base(description)
		{
			Name = name;
		}

		public string Name { get; }
	}
}