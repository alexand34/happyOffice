using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Newtonsoft.Json;
using Office.BusinessLogic;
using Office.Data.Entities;

namespace Office.API.Controllers
{
    public class IndexController : ApiController
    {
        FaceServiceClient faceServiceClient = new FaceServiceClient("2554672d2fad4aef9238a7476a7460d1",
            "https://westus.api.cognitive.microsoft.com/face/v1.0");
        StoreImagesService sis = new StoreImagesService();

        public IndexController()
        {
            
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Index/status")]
        public IHttpActionResult Status()
        {
            return Ok(true);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Index/trainAll")]
        public async Task<IHttpActionResult> TrainAll()
        {
            var personGroups = await faceServiceClient.ListPersonGroupsAsync();
            foreach (var personGroup in personGroups)
            {
                await faceServiceClient.TrainPersonGroupAsync(personGroup.PersonGroupId);
            }
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Index/recognize")]
        public async Task<IHttpActionResult> Recognize()
        {
            string path = HttpContext.Current.Server.MapPath("~/Images/ToRecognize/");
            var personGroups = await faceServiceClient.ListPersonGroupsAsync();
            var files = HttpContext.Current.Request.Files;
            List<string> identity = new List<string>();
            if (files.Count < 1)
            {
                identity.Add("No photos uploaded");
                return Ok(identity);
            }
            try
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string filePath = sis.UploadFile(file, path);

                    Stream s = File.OpenRead(filePath);
                    var faces = await faceServiceClient.DetectAsync(s);
                    var faceIds = faces.Select(face => face.FaceId).ToArray();

                    bool recognized = false;

                    foreach (var personGroup in personGroups)
                    {
                        if (!recognized)
                        {
                            var results = await faceServiceClient.IdentifyAsync(personGroup.PersonGroupId, faceIds);
                            foreach (var identifyResult in results)
                            {
                                if (identifyResult.Candidates.Length == 0)
                                {
                                    identity.Add("No one indetified!");
                                }
                                else
                                {
                                    var candidateId = identifyResult.Candidates[0].PersonId;
                                    var person =
                                        await faceServiceClient.GetPersonAsync(personGroup.PersonGroupId, candidateId);

                                    identity.Add(string.Format("Identified as {0}", person.Name));
                                    recognized = true;
                                }
                            }
                        }
                        else
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(identity);
        }
    }
}
