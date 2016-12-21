using System;
using System.Web.Http;
using JetBrains.Annotations;
using KP.Api.Controllers.Playground;
using KP.Service;
using KP.Storage.Domain;


namespace KP.Api.Controllers
{
    [RoutePrefix("v1/staffobjects")]
    public class StaffObjectsController : ApiController
    {
        private readonly IManningTableService manningTableService;

        public StaffObjectsController(IManningTableService manningTableService)
        {
            this.manningTableService = manningTableService;
        }

        [Route("")]
        [HttpPost]
        [PlaygroundMethod("Создание объекта штатного расписания")]
        public StaffObject CreateStaffObject(
            [NotNull] [PlaygroundParameter("Дата создания")] DateTime dateBegin,
            [NotNull][PlaygroundParameter("ТипОбъекта")] StaffObjectType objectType)
        {
            return manningTableService.CreateStaffObject(dateBegin, objectType);
        }

    }
}