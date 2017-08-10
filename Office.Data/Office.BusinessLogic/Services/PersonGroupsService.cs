using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

namespace Office.BusinessLogic.Services
{
    public class PersonGroupsService
    {
        FaceServiceClient _faceServiceClient = new FaceServiceClient("2554672d2fad4aef9238a7476a7460d1",
            "https://westus.api.cognitive.microsoft.com/face/v1.0");

        public async Task<PersonGroup[]> GetAll()
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

        public async Task TrainAll()
        {
            var personGroups = await _faceServiceClient.ListPersonGroupsAsync();
            foreach (var pg in personGroups)
            {
                await _faceServiceClient.TrainPersonGroupAsync(pg.PersonGroupId);
            }
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
