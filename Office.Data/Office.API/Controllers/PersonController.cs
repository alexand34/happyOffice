using Microsoft.ProjectOxford.Face;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.ProjectOxford.Face.Contract;
using Newtonsoft.Json;
using Office.BusinessLogic;
using Office.Data.Entities;

namespace Office.API.Controllers
{
    public class PersonController : ApiController
    {
        FaceServiceClient faceServiceClient = new FaceServiceClient("2554672d2fad4aef9238a7476a7460d1",
            "https://westus.api.cognitive.microsoft.com/face/v1.0");
        StoreImagesService sis = new StoreImagesService();
        //StoreImagesService storeImagesService = new StoreImagesService();
        public PersonController()
        {

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Person/getAll/{personGroupId}")]
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
        [AllowAnonymous]
        [Route("api/Person/get/{personGroupId}/{personId}")]
        public async Task<IHttpActionResult> GetPerson(string personGroupId, string personId)
        {
            try
            {
                Guid person = new Guid(personId);
                var foundPerson = await faceServiceClient.GetPersonAsync(personGroupId, person);
                return Ok(foundPerson);
            }
            catch (Exception ex)
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
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdatePerson(string personGroupId, string personId, string name,
            string position)
        {
            try
            {
                Guid person = new Guid(personId);
                await faceServiceClient.UpdatePersonAsync(personGroupId, person, name, position);
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Person/addPerson")]
        public async Task<IHttpActionResult> addPerson()
        {
            string path = HttpContext.Current.Server.MapPath("~/Images/Files/");

            var model = HttpContext.Current.Request.Form["NewUser"];

            var user = JsonConvert.DeserializeObject<NewUser>(model);

            if (user == null)
            {
                return BadRequest("no user data defined");
            }

            var createdUser = await faceServiceClient.CreatePersonAsync(user.GroupId, user.Name, user.Position);
            var files = HttpContext.Current.Request.Files;
            try
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string filePath = sis.UploadFile(file, path);
                    Stream s = File.OpenRead(filePath);
                    await faceServiceClient.AddPersonFaceAsync(user.GroupId, createdUser.PersonId, s);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }

    }
}
