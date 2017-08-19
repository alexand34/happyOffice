using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Office.Common;

namespace Office.BusinessLogic.Services
{
    public class RecognitionService
    {
        FaceServiceClient _faceServiceClient = new FaceServiceClient(Config.ApiKey, Config.EndPointUrl);

        public async Task TrainAll()
        {
            var personGroups = await _faceServiceClient.ListPersonGroupsAsync();
            foreach (var pg in personGroups)
            {
                await _faceServiceClient.TrainPersonGroupAsync(pg.PersonGroupId);
                TrainingStatus trainingStatus = null;
                while (true)
                {
                    trainingStatus = await _faceServiceClient.GetPersonGroupTrainingStatusAsync(pg.PersonGroupId);

                    if (trainingStatus.Status.ToString() != "running")
                    {
                        break;
                    }
                    await Task.Delay(1000);
                }
            }
        }

        public async Task<string> Recognize(HttpFileCollection files, string personGroupId)
        {
            string recognitionString = "We have recognized: ";
            int guests = 0;
            int knownPeople = 0;
            if (files.Count < 1)
            {
                recognitionString = "None of the files has come down to us :) Try again, please.";
                return recognitionString;
            }
            try
            {
                Stream s = files[0].InputStream;
                var faces = await _faceServiceClient.DetectAsync(s);
                var faceIds = faces.Select(face => face.FaceId).ToArray();
                var results = await _faceServiceClient.IdentifyAsync(personGroupId, faceIds);
                foreach (var result in results)
                {
                    if (result.Candidates.Length == 0)
                    {
                        guests += 1;
                        recognitionString = $"We have found {guests} guests on the photo and no employees";
                        return recognitionString;
                    }
                    var candidateId = result.Candidates[0].PersonId;
                    var person = await _faceServiceClient.GetPersonAsync(personGroupId, candidateId);
                    knownPeople++;
                    recognitionString += person.Name + " ";
                }
                if (guests != 0)
                {
                    recognitionString += $"and {guests} guests";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return recognitionString;
        }
    }
}
