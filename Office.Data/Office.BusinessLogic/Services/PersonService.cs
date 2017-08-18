using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Office.Common;
using Office.Data.Entities;

namespace Office.BusinessLogic.Services
{
    public class PersonService
    {
        FaceServiceClient _faceServiceClient = new FaceServiceClient(Config.ApiKey, Config.EndPointUrl);

        public PersonService()
        {
            
        }

        public async Task<IList<Person>> GetAll(string personGroupId)
        {
            var persons = await _faceServiceClient.ListPersonsAsync(personGroupId);
            return persons;
        }

        public async Task AddPerson(HttpFileCollection files, NewUser user)
        {
            try
            {
                var createdUser = await _faceServiceClient.CreatePersonAsync(user.GroupId, user.Name, user.Position);

                for(int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    await _faceServiceClient.AddPersonFaceAsync(user.GroupId, createdUser.PersonId, file.InputStream);
                }
            }
            catch(Exception ex)
            {

            }
        }

        public async Task DeletePerson(string personGroupId, string personId)
        {
            var id = new Guid(personId);
            await _faceServiceClient.DeletePersonAsync(personGroupId, id);
        }
    }
}
