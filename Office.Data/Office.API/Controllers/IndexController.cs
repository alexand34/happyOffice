using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

namespace Office.API.Controllers
{
    public class IndexController : ApiController
    {
        public IndexController()
        {
            
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Index/trainAll")]
        public IHttpActionResult TrainAll()
        {
            
            return Ok();
        }
    }
}
