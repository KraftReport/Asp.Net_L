using Microsoft.AspNetCore.Http;
using RestSharp;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealTimeStreamingDemo.Streaming
{
    public class StreamingService
    {
        private const string UploadUrl = "https://dev.vdocipher.com/api/videos/importUrl";
        private const string ApiKey = "lxm2o3GSann73mugCWMMX43GEge2jrngBVQvm7H3EDmJDB0PuurktLbLzSe5jKMd"; 
         

        public async Task<bool> UploadVideoToVdoCipherServer(UploadRequestModel uploadRequestModel)
        { 
            using(var _httpClient = new HttpClient())
            using(var form = new MultipartFormDataContent())
            {
                _httpClient.DefaultRequestHeaders.ConnectionClose = false; 
                _httpClient.Timeout = TimeSpan.FromMinutes(15);
                var fs = /*File.OpenRead(uploadRequestModel.VideoUrl);*/ uploadRequestModel.file.OpenReadStream(); 
                form.Add(new StringContent(uploadRequestModel.Policy), "policy");
                form.Add(new StringContent(uploadRequestModel.Key), "key");
                form.Add(new StringContent(uploadRequestModel.Signature), "x-amz-signature");
                form.Add(new StringContent(uploadRequestModel.Algorithm), "x-amz-algorithm");
                form.Add(new StringContent(uploadRequestModel.Date), "x-amz-date");
                form.Add(new StringContent(uploadRequestModel.Credential), "x-amz-credential");
                form.Add(new StringContent("201"), "success_action_status");
                form.Add(new StringContent(""), "success_action_redirect");
                form.Add(new StreamContent(fs), "file", uploadRequestModel.title);
              //  form.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                var response = await _httpClient.PostAsync(uploadRequestModel.UploadLink, form);
                return response.IsSuccessStatusCode;
            }
        }



        public string GetOtpAndPlaybackInfo(string videoId)
        {
            var _restClient = new RestClient($"https://dev.vdocipher.com");
            var request = new RestRequest($"api/videos/{videoId}/otp",Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Apisecret {ApiKey}");
            var body = new { ttl = 300 };
            request.AddBody(body);
           // request.AddParameter("undefined", "{\n\t\"ttl\":300\n}", ParameterType.RequestBody);
            var response = _restClient.Execute(request);
            return response.Content;
        }

        public async Task<string> UploadVideo(/*string videoUrl,*/string title,IFormFile file)
        {
            var credentials = GetUploadCredentials(title);
            var dto = new UploadRequestModel
            {
                file = file,
                Algorithm = credentials.ClientPayload.XAmzAlgorithm,
                Credential = credentials.ClientPayload.XAmzCredential,
                Date = credentials.ClientPayload.XAmzDate,
                Key = credentials.ClientPayload.Key,
                Policy = credentials.ClientPayload.Policy,
                Signature = credentials.ClientPayload.XAmzSignature,
                UploadLink = credentials.ClientPayload.UploadLink,
                title = title,
              /*  VideoUrl = videoUrl,*/
            };
            await UploadVideoToVdoCipherServer(dto);
            return credentials.VideoId;
        }


         

        private CredentialResponseModel GetUploadCredentials(string title)
        {
            using(var client = new RestClient("https://dev.vdocipher.com"))
            {
                var request = new RestRequest($"api/videos?title={title}",Method.Put); 
                request.AddHeader("Authorization", $"Apisecret {ApiKey}");
                var response = client.Execute(request);
                var data = response.Content;
                var jsonDocument = JsonDocument.Parse(data);
                var clientPayload = jsonDocument.RootElement.GetProperty("clientPayload");
                var videoId = jsonDocument.RootElement.GetProperty("videoId").GetString();
                var clientPayloadModel = new ClientPayload
                {
                    XAmzAlgorithm = clientPayload.GetProperty("x-amz-algorithm").GetString(),
                    XAmzCredential = clientPayload.GetProperty("x-amz-credential").GetString(),
                    XAmzDate = clientPayload.GetProperty("x-amz-date").GetString(),
                    Key = clientPayload.GetProperty("key").GetString(),
                    Policy = clientPayload.GetProperty("policy").GetString(),
                    UploadLink = clientPayload.GetProperty("uploadLink").GetString(),
                    XAmzSignature = clientPayload.GetProperty("x-amz-signature").GetString()
                };
                return new CredentialResponseModel
                {
                    ClientPayload = clientPayloadModel,
                    VideoId = videoId
                }; 
            }
        }

    }
}
