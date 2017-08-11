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
using Office.BusinessLogic.Services;
using Office.Data.Entities;

namespace Office.API.Controllers
{
    public class IndexController : ApiController
    {
        RecognitionService _recognitionService = new RecognitionService();

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
            try
            {
                await _recognitionService.TrainAll();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/Index/recognize/{personGroupId}")]
        public async Task<IHttpActionResult> Recognize(string personGroupId)
        {
            try
            {
                var files = HttpContext.Current.Request.Files;
                string personString = await _recognitionService.Recognize(files, personGroupId);
                return Ok(personString);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
