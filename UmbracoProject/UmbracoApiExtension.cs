using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Management.Controllers;
using Umbraco.Cms.Api.Management.Routing;

namespace UmbracoProject
{
    [VersionedApiBackOfficeRoute("isOk")]
    [ApiExplorerSettings(GroupName = "Morgana")]
    public class MiApiController : ManagementApiControllerBase
    {
        /// <summary>
        /// Checks if the value is OK.
        /// </summary>
        /// <param name="value">A boolean value indicating the status to check.</param>
        /// <returns>Returns OK if true, otherwise BadRequest.</returns>
        [HttpGet]
        public IActionResult CheckIsOk([FromQuery] bool value)
        {
            return value ? Ok() : BadRequest();
        }
    }
}
