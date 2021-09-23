using KaaDevAlert.Models;
using KaaDevAlert.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KaaDevAlert.Service
{
    public interface ILineNotiService
    {
        void SendLineNoti(string SelectedPost);

    }
    public class LineNotiService : ILineNotiService
    {
        private readonly IConfigurationRepository configurationRepository;

        public LineNotiService(IConfigurationRepository configurationRepository)
        {
            this.configurationRepository = configurationRepository;
        }
        string URL_WEBHOST = "https://notify-api.line.me/api/notify";
       


        //public void SendLineNoti()
        public void SendLineNoti(string SelectedPost)
        {
          
            try
            {

                //var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
                var request = (HttpWebRequest)WebRequest.Create(URL_WEBHOST);
                var TOKEN = configurationRepository.GetKeyNumber("LineToken");
                var Link = configurationRepository.GetKeyNumber("Linkmeeting");
                //var postData = string.Format("message={0}", SelectedPost);
                var postData = $"message={SelectedPost}  Link :{Link.Value}";
                var data = Encoding.UTF8.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                //request.Headers.Add("Authorization", "Bearer " + TOKEN.Value);
                request.Headers.Add("Authorization", "Bearer " + TOKEN.Value );




                using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length);

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
      
    }
}
        
    

