using System.Net.Http;
using Newtonsoft.Json.Linq;
using Nubot.Adapters;

namespace Nubot.Scripts
{
    public class GoogleImagesModule : RobotModule
    {
        public GoogleImagesModule()
        {
            Respond["(image|img)( me)? (.*)"] = ImageMe;
        }

        public void ImageMe(IMessageChannel msg, string text)
        {
            /*    
                  cb = animated if typeof animated == 'function'
                  cb = faces if typeof faces == 'function'
                  q = v: '1.0', rsz: '8', q: query, safe: 'active'
                  q.imgtype = 'animated' if typeof animated is 'boolean' and animated is true
                  q.imgtype = 'face' if typeof faces is 'boolean' and faces is true
                  msg.http('http://ajax.googleapis.com/ajax/services/search/images')
                    .query(q)
                    .get() (err, res, body) ->
                      images = JSON.parse(body)
                      images = images.responseData?.results
                      if images?.length > 0
                        image  = msg.random images
                        cb "#{image.unescapedUrl}#.png"
             */

            var client = new HttpClient();
            var response = client.GetAsync(string.Format("http://ajax.googleapis.com/ajax/services/search/images?v=1.0&rsz=8&safe=active&q={0}", "cheese")).Result;
            var body = response.Content.ReadAsStringAsync().Result;
            
            dynamic json = JObject.Parse(body);
            msg.Send(json.responseData.results[0].unescapedUrl.Value);
        }
    }
}