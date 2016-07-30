using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Portal.Data
{
    public class ApiStoreClient : IDisposable
    {
        private readonly HttpClient _client;
        private static volatile ApiStoreClient instance;
        private static Mutex mutex = new Mutex();

        private readonly string _apiUserName;
        private readonly string _apiPassword;
        private string _jwtToken;

        private ApiStoreClient()
        {
            _client = new HttpClient();
            _apiUserName = ApiStoreClientConfig.ApiUserName;
            _apiPassword = ApiStoreClientConfig.ApiPassword;
            InitHttpClient();
        }

        //private ApiStoreClient(string uri, string acceptType = "application/json")
        //{
        //    _client = new HttpClient();

        //    InitHttpClient(uri, acceptType);
        //}

        public static ApiStoreClient Instance
        {
            get
            {
                if (instance == null)
                {
                    mutex.WaitOne();

                    if (instance == null)
                        instance = new ApiStoreClient();

                    mutex.ReleaseMutex();
                }
                return instance;
            }
        }

        private void InitHttpClient()
        {
            _client.BaseAddress = new Uri(ApiStoreClientConfig.Uri);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiStoreClientConfig.AcceptType));
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response) where T : class
        {
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    T result;
                    result = await response.Content.ReadAsAsync<T>();
                    return result;
                }
                else
                {
                    return null;
                    //throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                //var jsonString = await response.Content.ReadAsStringAsync();
                //JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                // var objectData = (T)jsonSerializer.DeserializeObject(jsonString);
                //var objectData = (T)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString, typeof(T));
                //var objectData = JsonConvert.DeserializeObject<T>(jsonString);
                //var data = (T)objectData;

                throw;
            }
        }

        private async Task<bool> Reconnect()
        {
            if (!string.IsNullOrEmpty(_jwtToken))
            {
                var response = await Verify();

                if (response.IsSuccessStatusCode) return true;

                if (!(response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                      response.StatusCode == System.Net.HttpStatusCode.Forbidden))
                    throw new HttpRequestException(response.ToString());
            }

            _client.DefaultRequestHeaders.Authorization = null;
            var authenticationRequest = new AuthenticationRequest { UserName = _apiUserName, Password = _apiPassword };

            var resp = await _client.PostAsJsonAsync("/api/authenticate", authenticationRequest);

            if (resp.IsSuccessStatusCode)
            {
                AuthenticationResponse auth = await resp.Content.ReadAsAsync<AuthenticationResponse>();

                _jwtToken = auth.AccessToken;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("AccessToken", _jwtToken);

                return true;
            }
            else
            {
                throw new HttpRequestException(resp.ToString());
            }
        }
        private async Task<HttpResponseMessage> Verify()
        {
            return await _client.GetAsync("api/authenticate/verify");
        }

        public async Task<T> GetAsync<T>(string requestUri) where T : class
        {
            var response = await _client.GetAsync(requestUri);

            var result = await ProcessResponse<T>(response);

            if (result == null)
            {
                await Reconnect();

                response = null;
                result = null;
                response = await _client.GetAsync(requestUri);
                result = await ProcessResponse<T>(response);

                if (result == null)
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
            }

            return result;
        }

        public async Task<T2> PostAsync<T, T2>(string requestUri, T data)
            where T : class
            where T2 : class
        {
            var response = await _client.PostAsJsonAsync(requestUri, data);

            var result = await ProcessResponse<T2>(response);

            if (result == null)
            {
                await Reconnect();
                response = null;
                result = null;
                response = await _client.PostAsJsonAsync(requestUri, data);
                result = await ProcessResponse<T2>(response);

                if (result == null)
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
            }

            return result;
        }

        public async Task<T2> PutAsync<T, T2>(string requestUri, T data)
             where T : class
            where T2 : class
        {
            var response = await _client.PutAsJsonAsync(requestUri, data);

            var result = await ProcessResponse<T2>(response);

            if (result == null)
            {
                await Reconnect();
                response = null;
                result = null;
                response = await _client.PutAsJsonAsync(requestUri, data);
                result = await ProcessResponse<T2>(response);

                if (result == null)
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
            }

            return result;
        }

        public async Task<T> DeleteAsync<T>(string requestUri)
            where T : class
        {
            //InitHttpClient(Uri, acceptType);

            // HTTP DELETE

            var response = await _client.DeleteAsync(requestUri);
            var result = await ProcessResponse<T>(response);

            if (result == null)
            {
                await Reconnect();
                response = null;
                result = null;
                response = await _client.DeleteAsync(requestUri);
                result = await ProcessResponse<T>(response);

                if (result == null)
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
            }

            return result;
        }


        private bool _isDisposed = false;
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _client.Dispose();
            }
            _isDisposed = true;
        }
    }

    internal class AuthenticationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    internal class AuthenticationResponse
    {
        public string AccessToken { get; set; }
        public string ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
    }
}

