using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using JetBrains.Annotations;
using KP.Api.Core.Helpers;

namespace KP.Api.Controllers.Playground
{
	public class PlaygroundService : IPlaygroundService
	{
		public PlaygroundService()
		{
			
		}

		[NotNull]
		public PlaygroundMethodDesc[] GetStaffObjectsMethods([NotNull] HttpRequestMessage request)
		{
			var routePrefix = typeof(StaffObjectsController).GetCustomAttribute<RoutePrefixAttribute>().Prefix;
			var methodInfos = typeof(StaffObjectsController).GetMethods(BindingFlags.Public | BindingFlags.Instance)
				.Where(x => x.GetCustomAttribute<PlaygroundMethodAttribute>() != null);
			var result = new List<PlaygroundMethodDesc>();
			foreach (var methodInfo in methodInfos)
			{
				var route = methodInfo.GetCustomAttribute<RouteAttribute>()?.Template ?? "";
				var parameterInfos = methodInfo.GetParameters();
				var parameters = new List<PlaygroundParameterDesc>();
				foreach (var parameterInfo in parameterInfos)
				{
					parameters.Add(new PlaygroundParameterDesc
					{
						Name = parameterInfo.Name,
						Description = parameterInfo.GetCustomAttribute<PlaygroundParameterAttribute>()?.Description,
						FromUriTemplate = route.Contains($"{{{parameterInfo.Name}}}"),
						Optional = parameterInfo.IsOptional,
						Values = GetValues(parameterInfo.Name)
					});
				}
				var bodyDescription = methodInfo.GetCustomAttribute<PlaygroundBodyAttribute>();
				if (bodyDescription != null)
				{
					parameters.Add(new PlaygroundParameterDesc
					{
						Name = bodyDescription.Name,
						Description = bodyDescription.Description,
						Optional = true,
						FromBody = true
					});
				}

				var apiMethodDesc = new PlaygroundMethodDesc
				{
					Name = methodInfo.Name,
					Description = methodInfo.GetCustomAttribute<PlaygroundMethodAttribute>().Description,
					Types = GetMethodTypes(methodInfo),
					UriTemplate = request.BuildExternalUrl(routePrefix.TrimEnd('/') + "/" + route.TrimStart('/')),
					Parameters = parameters.ToArray(),
					IsContent = methodInfo.GetCustomAttribute<PlaygroundMethodAttribute>().IsContent
				};
				result.Add(apiMethodDesc);
			}
			return result.ToArray();
		}

		[CanBeNull]
		private string[] GetValues([NotNull] string parameterName)
		{
            switch (parameterName)
            {
                // todo возвращать для драфта свой набор, для документа - свой
                case "objectType":
                    return new [] { "StaffOrganization", "StaffDepartment", "StaffPosition" };
                default:
                    return null;
            }
        }

		[NotNull]
		private static string[] GetConstants([NotNull] Type type)
		{
			return type.GetFields().Where(f => f.IsLiteral).Select(f => f.GetRawConstantValue()).OfType<string>().ToArray();
		}

		[NotNull]
		private static string[] GetMethodTypes([NotNull] MemberInfo methodInfo)
		{
			var methodProviders = methodInfo.GetCustomAttributes().OfType<IActionHttpMethodProvider>().ToList();
			return methodProviders.Any()
				? methodProviders.SelectMany(x => x.HttpMethods).Select(x => x.Method).Distinct().ToArray()
				: new[] { HttpMethod.Get.Method };
		}
	}
}