﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web.Provider;
using Common;
using LykkeWalletApp.Services;

namespace LykkeWalletApp.ApiAccess
{
    public class ApiErrorModel
    {
        public int Code { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }
    }

    public class ApiResponseModel 
    {

        public ApiErrorModel Error { get; set; }
    }

    public class ApiException : Exception
    {
        public ApiException(ApiErrorModel error):base(error.Message)
        {
            ErrorModel = error;
        }

        public ApiErrorModel ErrorModel { get; set; }


        public static void CheckException(string json)
        {
            var errorModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponseModel>(json);

            if (errorModel.Error != null)
                throw new ApiException(errorModel.Error);

        }
    }

    public class ApiResponseModel<T> : ApiResponseModel
    {
        public T Result { get; set; }
    }


    public partial class SrvApiAccess
    {


        private const string Url = "https://lykke-api-dev.azurewebsites.net/api/";

        public static string CurrentToken;

        private static async Task<string> DoGetHttpRequestAsync(string path, object data)
        {

                var q = data == null ? "" : "?" + data.FormatUrlString();

                var webRequest = WebRequest.Create(Url + path + q);
                webRequest.Method = "GET";
                webRequest.ContentType = "application/json";

                if (CurrentToken != null)
                    webRequest.Headers["Authorization"] = "Bearer " + CurrentToken;


                var webResponse = await webRequest.GetResponseAsync();

                var receiveStream = webResponse.GetResponseStream();

                if (receiveStream == null)
                    return null;

                var sr = new StreamReader(receiveStream);
                var result = await sr.ReadToEndAsync();
                ApiException.CheckException(result);
                return result;


        }

        private async Task<T> DoGetRequestAsync<T>(string path, object data = null)
        {
            try
            {
                var response = await DoGetHttpRequestAsync(path, data);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponseModel<T>>(response);
                return result.Result;
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                    SaveToken(null);
                throw;
            }
        }


        private static async Task<string> DoPostHttpRequestAsync(string path, object data)
        {
                var webRequest = (HttpWebRequest)WebRequest.Create(Url + path);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";

                if (CurrentToken != null)
                    webRequest.Headers["Authorization"] = "Bearer " + CurrentToken;




                var stream = await webRequest.GetRequestStreamAsync();

                if (data == null)
                {
                    var me = new MemoryStream();
                    me.WriteTo(stream);
                }
                else
                {
                    var dataToSend = Newtonsoft.Json.JsonConvert.SerializeObject(data).ToUtf8ByteArray();
                    stream.Write(dataToSend, 0, dataToSend.Length);
                }


                var webResponse = await webRequest.GetResponseAsync();

                var receiveStream = webResponse.GetResponseStream();

                if (receiveStream == null)
                    return null;

                var sr = new StreamReader(receiveStream);


                var result = await sr.ReadToEndAsync();
                ApiException.CheckException(result);
                return result;


 

        }

        private async Task DoPostRequestAsync(string path, object data = null)
        {
            try
            {
                await DoPostHttpRequestAsync(path, data);
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                    SaveToken(null);
                throw;
            }
        }

        private async Task<T> DoPostRequestAsync<T>(string path, object data = null)
        {
            try
            {
                var response = await DoPostHttpRequestAsync(path, data);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponseModel<T>>(response);
                return result.Result;
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                    SaveToken(null);
                throw;
            }
        }

        public void SetToken(string token)
        {
            CurrentToken = token;
        }
    }
}
