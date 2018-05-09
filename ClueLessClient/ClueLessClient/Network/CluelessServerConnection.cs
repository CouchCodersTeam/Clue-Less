using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using static ClueLessClient.Network.RequestModels;

namespace ClueLessClient.Network
{
    class CluelessServerConnection
    {
        private static readonly HttpClient client = new HttpClient();
        
        private CluelessServerConnection(string host, int port)
        {
            UriBuilder builder = new UriBuilder();
            builder.Host = host;
            builder.Port = port;

            client.BaseAddress = builder.Uri;
            client.DefaultRequestHeaders.Accept.Add(item: new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static CluelessServerConnection instance = null;

        public static CluelessServerConnection getConnection()
        {
            if (instance == null)
                throw new Exception("Http client not initialized. Call getConnection(string, int) first and once.");
            return instance;
        }

        public static CluelessServerConnection getConnection(string host, int port)
        {
            if (instance != null)
                throw new Exception("Http client already initialized. Call getConnection(string, int) once.");

            instance = new CluelessServerConnection(host, port);
            return instance;
        }

        public bool registerAsPlayer(string playerName)
        {
            var data = new PlayerNameModel { PlayerName = playerName};
            var json = toJson(data);

            var response = client.PostAsync("/auth_code", new StringContent(json, Encoding.UTF8, "application/json")).Result;

            if (response.IsSuccessStatusCode)
            {
                var player = fromJsonAsync<PlayerNameModel>(response).Result;

                // Set header data for player
                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Authorization", player.AuthCode);
                return true;
            }

            return false;
        }


        private string toJson<T>(T obj)
        {
            //Create a stream to serialize the object to.  
            MemoryStream ms = new MemoryStream();

            // Serializer the User object to the stream.  
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(ms, obj);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        private async System.Threading.Tasks.Task<T> fromJsonAsync<T>(HttpResponseMessage data)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            var stream = await data.Content.ReadAsStreamAsync();
            T val = (T)ser.ReadObject(stream);

            return val;
        }
    }
}
