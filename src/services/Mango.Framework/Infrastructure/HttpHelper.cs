using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
namespace Mango.Framework.Infrastructure
{
    public class HttpHelper
    {
        public string Post(string requestUri)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpContent httpContent = new HttpRequestMessage();
                    httpClient.PostAsync(requestUri, httpContent);
                    return null;
                }
            }
            catch 
            {
                return null;
            }
        }
    }
}
