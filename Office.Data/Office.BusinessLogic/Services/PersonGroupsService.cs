using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Office.Common;

namespace Office.BusinessLogic.Services
{
    public class PersonGroupsService
    {
        FaceServiceClient _faceServiceClient = new FaceServiceClient(Config.ApiKey, Config.EndPointUrl);

        public async Task<IList<PersonGroup>> GetAll()
        {
            var personGroups = await _faceServiceClient.ListPersonGroupsAsync();
            return personGroups;
        }

        public async Task<PersonGroup> Get(string id)
        {
            var personGroup = await _faceServiceClient.GetPersonGroupAsync(id);
            return personGroup;
        }

        public async Task AddPersonGroup(PersonGroup group)
        {
            await _faceServiceClient.CreatePersonGroupAsync(group.PersonGroupId, group.Name);
        }

        public async Task TrainById(string id)
        {
            await _faceServiceClient.TrainPersonGroupAsync(id);
        }

        public async Task DeletePersonGroup(string id)
        {
            await _faceServiceClient.DeletePersonGroupAsync(id);
        }
    }
}
