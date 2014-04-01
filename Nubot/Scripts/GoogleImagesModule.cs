using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Nubot.Adapters;

namespace Nubot.Scripts
{
    public class GoogleImagesModule : RobotModule
    {
        public GoogleImagesModule()
        {
            const string api = "http://ajax.googleapis.com/ajax/services/search/images?v=1.0&rsz=8&safe=active&";

            Respond["(image|img)( me)? (.*)"] = (msg, text) => msg.Send(SearchGoogleForImage(msg, string.Format("{0}q={1}", api, text)));
            Respond["animate( me)? (.*)"] = (msg, text) => msg.Send(SearchGoogleForImage(msg, string.Format("imgtype=animated&{0}q={1}", api, text)));
        }

        public string SearchGoogleForImage(IMessageChannel msg, string imageUri)
        {
            var client = new HttpClient();
            var response = client.GetAsync(imageUri).Result;
            var body = response.Content.ReadAsStringAsync().Result;
            
            dynamic json = JObject.Parse(body);
            var randomImageId = new Random().Next(0, json.responseData.results.ToObject<List<dynamic>>().Count);
            var image = json.responseData.results[randomImageId];

            return image.unescapedUrl.Value.ToString();
        }
    }
}