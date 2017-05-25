using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotNetStandardSpotifyWebApi.Helpers {
    public static class WebRequestHelpers {

        internal static HttpClient Client { get; } = new HttpClient();

        #region Spotify OAuth HttpRequestMessage helpers
        internal static HttpRequestMessage SetupRequest(string uri, string authorizationToken) {
            return SetupRequest(uri, "Bearer", authorizationToken, HttpMethod.Get);
        }

        internal static HttpRequestMessage SetupRequest(string uri, string authorizationToken, HttpMethod method) {
            return SetupRequest(uri, "Bearer", authorizationToken, method);
        }

        internal static HttpRequestMessage SetupRequest(string uri, string credientialKey, string credentialValue, HttpMethod method) {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Headers.Authorization = new AuthenticationHeaderValue(credientialKey, credentialValue);
            request.RequestUri = new Uri(uri);
            request.Method = method;
            return request;
        }
        #endregion

        #region Json Parse Helpers
        internal static async Task<JToken> ParseJsonResponse(HttpContent content) {
            string stringContent = await content.ReadAsStringAsync();
            return JToken.Parse(stringContent);
            // return JObject.Parse(stringContent);
        }
        #endregion


    }
}
