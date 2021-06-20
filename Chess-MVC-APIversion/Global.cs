using Chess_MVC_APIversion.Models;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;



namespace Chess_MVC_APIversion
{
    public static class Global
    {
        public static PipeServer pipe = new PipeServer();
        public static bool firstClick = true;
        public static int firstx = 0;
        public static int firsty = 0;
        public static Chess chess = new Chess();
        public static string displayMsg = "";
        public static List<string> caped = new List<string>();
        public static string sendJSON(string n_header, int n_x, int n_y, string n_piece) 
        {
            string ResponseString = "";
            HttpWebResponse response = null;
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://3.12.160.7/API13/api/Instruction");
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "POST";
                Instruction obj = new Instruction() { header = n_header, x = n_x, y = n_y, piece = n_piece };
               
                JavaScriptSerializer jss = new JavaScriptSerializer();
                // serialize into json string
                var myContent = jss.Serialize(obj);

                var data = Encoding.ASCII.GetBytes(myContent);

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = data.Length;

                using (var stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                response = (HttpWebResponse)httpWebRequest.GetResponse();

                ResponseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)ex.Response;
                    ResponseString = "Some error occured: " + response.StatusCode.ToString();
                }
                else
                {
                    ResponseString = "Some error occured: " + ex.Status.ToString();
                }
            }
            Models.Response r  = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Response>(ResponseString);
            return r.header;
        }

        public static string sendJSON(string n_header)
        {
            string ResponseString = "";
            HttpWebResponse response = null;
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://3.12.160.7/API13/api/Instruction");
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "POST";
                Instruction obj = new Instruction() { header = n_header };

                JavaScriptSerializer jss = new JavaScriptSerializer();
                // serialize into json string
                var myContent = jss.Serialize(obj);

                var data = Encoding.ASCII.GetBytes(myContent);

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = data.Length;

                using (var stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                response = (HttpWebResponse)httpWebRequest.GetResponse();

                ResponseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)ex.Response;
                    ResponseString = "Some error occured: " + response.StatusCode.ToString();
                }
                else
                {
                    ResponseString = "Some error occured: " + ex.Status.ToString();
                }
            }
            Models.Response r = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Response>(ResponseString);
            return r.header;
        }

        public static void clearInput() 
        {
            Global.firstClick = true;
            Global.firstx = 0;
            Global.firsty = 0;
            Global.chess = new Chess();
            Global.displayMsg = "";
            Global.caped = new List<string>();
        }
    }
    static class SendEmails
    {

        public static async Task SendEmail(string email, string user)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient("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");
            var from = new EmailAddress("TimothyXue.dev@gmail.com", "Chess game");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(email, user);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
