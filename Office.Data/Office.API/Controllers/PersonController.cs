using Microsoft.ProjectOxford.Face;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Office.BusinessLogic;

namespace Office.API.Controllers
{
    public class PersonController : ApiController
    {
        FaceServiceClient faceServiceClient = new FaceServiceClient("2554672d2fad4aef9238a7476a7460d1", "https://westus.api.cognitive.microsoft.com/face/v1.0");
        StoreImagesService storeImagesService = new StoreImagesService();

        [HttpGet]
        public async Task<IHttpActionResult> GetAll(string personGroupId)
        {
            try
            {
                var persons = await faceServiceClient.ListPersonsAsync(personGroupId);
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPerson(string personGroupId,string personId)
        {
            try
            {

                Guid person = new Guid(personId);
                var foundPerson = await faceServiceClient.GetPersonAsync(personGroupId, person);

                return Ok(foundPerson);
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddPerson(string personGroupId, string name, string position)
        {
            try
            {
                storeImagesService.CreatePersonFolder(personGroupId, name);
                await faceServiceClient.CreatePersonAsync(personGroupId, name);
                return Ok();
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeletePerson(string personGroupId, string personId)
        {
            try
            {
                Guid person = new Guid(personId);
                await faceServiceClient.DeletePersonAsync(personGroupId, person);
                storeImagesService.DeletePersonFolder(personGroupId, personId);
                return Ok();
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdatePerson(string personGroupId, string personId, string name, string position)
        {
            try
            {
                Guid person = new Guid(personId);
                await faceServiceClient.UpdatePersonAsync(personGroupId, person, name, position);
                return Ok();
            }
            catch(Exception ex)
            {
                return Ok();
            }
        }
    }
}
