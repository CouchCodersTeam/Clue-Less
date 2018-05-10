using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Network
{
    class CluelessJsonContent : StringContent
    {
        public CluelessJsonContent(string json)
            : base(json, Encoding.UTF8, "application/json")
        {
        }
    }

    class Json
    {
        public static string toJson<T>(T obj)
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

        public static T fromJson<T>(HttpResponseMessage data)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            var stream = data.Content.ReadAsStreamAsync().Result;
            T val = (T)ser.ReadObject(stream);

            return val;
        }

    }
}
