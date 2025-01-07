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
                var fs = File.OpenRead(uploadRequestModel.VideoUrl);
                form.Add(new StringContent(uploadRequestModel.Policy), "policy");
                form.Add(new StringContent(uploadRequestModel.Key), "key");
                form.Add(new StringContent(uploadRequestModel.Signature), "x-amz-signature");
                form.Add(new StringContent(uploadRequestModel.Algorithm), "x-amz-algorithm");
                form.Add(new StringContent(uploadRequestModel.Date), "x-amz-date");
                form.Add(new StringContent(uploadRequestModel.Credential), "x-amz-credential");
                form.Add(new StringContent("201"), "success_action_status");
                form.Add(new StringContent(""), "success_action_redirect");
                form.Add(new StreamContent(fs), "file", Path.GetFileName(uploadRequestModel.VideoUrl));
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
            request.AddParameter("undefined", "{\n\t\"ttl\":300\n}", ParameterType.RequestBody);
            var response = _restClient.Execute(request);
            return response.Content;
        }
    }
}
