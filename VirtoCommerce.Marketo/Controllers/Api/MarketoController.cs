using System.Web.Http;

namespace VirtoCommerce.Marketo.Controllers.Api
{
    [RoutePrefix("api/marketo")]
    public class ManagedModuleController : ApiController
    {
        // GET: api/managedModule
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(new { result = "Hello world!" });
        }
    }
}
