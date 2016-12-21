namespace KP.Api.Controllers.Playground
{
	public class PlaygroundMethodAttribute : PlaygroundDescriptionAttributeBase
	{
		public PlaygroundMethodAttribute(string description)
			: base(description)
		{
		}

		public bool IsContent { get; set; }
	}
}