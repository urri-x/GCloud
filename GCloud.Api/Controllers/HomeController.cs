using System;
using System.Linq;
using System.Web.Http;
using KP.Api.Controllers.Playground;
using KP.Api.Core.Helpers;

namespace KP.Api.Controllers
{

	[RoutePrefix("")]
	public class HomeController : ApiController
	{
	    private readonly IPlaygroundService playgroundService;

	    public HomeController(IPlaygroundService playgroundService)
	    {
	        this.playgroundService = playgroundService;
	    }

        [Route("")]
        [HttpGet]
        public object Index()
        {
            return Request.View("~/Views/Playground.cshtml", new PlaygroundModel
            {
                StaffObjectsMethods = playgroundService.GetStaffObjectsMethods(Request)
            });
        }

        [Route("playground/v1/staffobjects/{method}")]
        [HttpGet]
        public object Method(string method)
        {
            return Request.View("~/Views/PlaygroundMethod.cshtml", new PlaygroundMethodModel
            {
                Method = playgroundService.GetStaffObjectsMethods(Request).Single(x => x.Name.Equals(method, StringComparison.OrdinalIgnoreCase))
            });
        }

        public class PlaygroundModel
        {
            public PlaygroundMethodDesc[] StaffObjectsMethods { get; set; }
        }

        public class PlaygroundMethodModel
        {
            public PlaygroundMethodDesc Method { get; set; }
        }
    }
}