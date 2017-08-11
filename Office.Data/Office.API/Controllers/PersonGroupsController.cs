using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Office.BusinessLogic;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Office.BusinessLogic.Services;

namespace Office.API.Controllers
{

    public class PersonGroupsController : ApiController
    {
        private readonly PersonGroupsService _personGroupsService = new PersonGroupsService();

        public PersonGroupsController()
        {

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/PersonGroups/getAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            return Ok(await _personGroupsService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/PersonGroups/get/{id}")]
        public async Task<IHttpActionResult> Get(string id)
        {
            return Ok(await _personGroupsService.Get(id));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/PersonGroups/add")]
        public async Task<IHttpActionResult> AddPersonGroup(PersonGroup personGroup)
        {
            try
            {
                await _personGroupsService.AddPersonGroup(personGroup);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/PersonGroups/delete/{id}")]
        public async Task<IHttpActionResult> DeletePersonGroup(string id)
        {
            try
            {
                await _personGroupsService.DeletePersonGroup(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
