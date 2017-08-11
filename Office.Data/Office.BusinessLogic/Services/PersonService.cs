using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

namespace Office.BusinessLogic.Services
{
    public class PersonService
    {
        FaceServiceClient _faceServiceClient = new FaceServiceClient("2554672d2fad4aef9238a7476a7460d1",
            "https://westus.api.cognitive.microsoft.com/face/v1.0");
        StoreImagesService sis = new StoreImagesService();

        public PersonService()
        {
            
        }

        public async Task<Person[]> GetAll(string personGroupId)
        {
            var persons = await _faceServiceClient.ListPersonsAsync(personGroupId);
            return persons;
        }
    }
}
