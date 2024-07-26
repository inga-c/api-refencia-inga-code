using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace IngaCode.TimeTracker.Web.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/time-trackers")]
    [Produces("application/json")]
    //[Authorize("access:workspace")]
    public class TimeTrackerController : ControllerBase
    {
    }
}
