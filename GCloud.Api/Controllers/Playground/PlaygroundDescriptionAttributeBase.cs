using System;

namespace KP.Api.Controllers.Playground
{
	public abstract class PlaygroundDescriptionAttributeBase : Attribute
	{
		protected PlaygroundDescriptionAttributeBase(string description)
		{
			Description = description;
		}

		public string Description { get; }
	}
}