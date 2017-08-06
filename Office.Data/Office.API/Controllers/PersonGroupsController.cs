using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Office.BusinessLogic;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Office.API.Controllers
{
    public class PersonGroupsController : ApiController
    {
        FaceServiceClient faceServiceClient = new FaceServiceClient("2554672d2fad4aef9238a7476a7460d1", "https://westus.api.cognitive.microsoft.com/face/v1.0");
        StoreImagesService storeImagesService = new StoreImagesService();

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var personGroups = await faceServiceClient.ListPersonGroupsAsync();
                return Ok(personGroups);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var personGroup = await faceServiceClient.GetPersonGroupAsync(id);
                return Ok(personGroup);
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddPersonGroup(PersonGroup personGroup)
        {
            try
            {
                storeImagesService.CreatePersonGroupFolder(personGroup.PersonGroupId);
                await faceServiceClient.CreatePersonGroupAsync(personGroup.PersonGroupId, personGroup.Name, personGroup.UserData);
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeletePersonGroup(string id)
        {
            try
            {
                await faceServiceClient.DeletePersonGroupAsync(id);
                storeImagesService.DeletePersonGroupFolder(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}
