using System;
using System.Collections.Generic;
using RoadStatus.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace RoadStatus.Core
{
    public class RoadAPIClient : ICallRoadAPI
    {
        private IConfiguration _config;
        private string _apiUrl;
        private string _appId;
        private string _appKey;

        private static readonly HttpClient client = new HttpClient();

        public RoadAPIClient(IConfiguration config)
        {
            _config = config;
            SetClient();
        }
        
        public async Task<RoadCorridor> GetRoadStatus(string roadId)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<RoadCorridor>));

            if (string.IsNullOrWhiteSpace(roadId))
            {
                throw new ArgumentNullException(nameof(roadId));
            }
            _apiUrl = _config["roadStatusApi"];
            _appId = _config["appId"];
            _appKey = _config["appKey"];

            var uri = new Uri($"{_apiUrl}{roadId}?app_id={_appId}&app_key={_appKey}");

            var response = await client.GetAsync($"{uri}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception($"{roadId} is not a valid road");
                }
                else
                {
                    throw new HttpRequestException();
                }
            }

            var obj = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<RoadCorridor>>(obj)[0];
        }

        private static void SetClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

