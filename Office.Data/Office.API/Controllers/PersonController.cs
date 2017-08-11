using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.ProjectOxford.Face.Contract;
using Newtonsoft.Json;
using Office.BusinessLogic;
using Office.BusinessLogic.Services;
using Office.Data.Entities;

namespace Office.API.Controllers
{
    public class PersonController : ApiController
    {
        PersonService _personService = new PersonService();
        public PersonController()
        {

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Person/getAll/{personGroupId}")]
        public async Task<IList<Person>> GetAll(string personGroupId)
        {
            return (await _personService.GetAll(personGroupId));
        }

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("api/Person/get/{personGroupId}/{personId}")]
        //public async Task<IHttpActionResult> GetPerson(string personGroupId, string personId)
        //{
        //    _personService.g
        //    try
        //    {
        //        Guid person = new Guid(personId);
        //        var foundPerson = await faceServiceClient.GetPersonAsync(personGroupId, person);
        //        return Ok(foundPerson);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //    }
        //}

        [HttpDelete]
        public async Task<IHttpActionResult> DeletePerson(string personGroupId, string personId)
        {
            try
            {
                await _personService.DeletePerson(personGroupId, personId);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpPost]
        //public async Task<IHttpActionResult> UpdatePerson(string personGroupId, string personId, string name, string position)
        //{
        //    try
        //    {
        //        Guid person = new Guid(personId);
        //        await faceServiceClient.UpdatePersonAsync(personGroupId, person, name, position);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok();
        //    }
        //}

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Person/addPerson")]
        public async Task<IHttpActionResult> AddPerson()
        {
            try
            {
                var model = HttpContext.Current.Request.Form["NewUser"];

                var user = JsonConvert.DeserializeObject<NewUser>(model);

                if (user == null)
                {
                    return BadRequest("no user data defined");
                }
                var files = HttpContext.Current.Request.Files;
                await _personService.AddPerson(files, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
