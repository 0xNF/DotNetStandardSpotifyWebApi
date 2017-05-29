using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.IO;
using Newtonsoft.Json.Linq;
using DotNetStandardSpotifyWebApi.Helpers;

namespace DotNetStandardSpotifyWebApi.Authorization {

    public static class AuthorizationCodeFlow {

        public const int stateLength = 16;
        private const string SpotifyAPITokenUrl = "https://accounts.spotify.com/api/token";
        
        public static string GenerateRandomString(int length) {
            Random _random = new Random();
            string text = "";
            string possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            for (int i = 0; i < length; i++) {
                int r = _random.Next() % possible.Length;
                text += possible[r];
            }
            return text;
        }

        public static string GetAuthState() {
            return GenerateRandomString(stateLength);
        }

        /// <summary>
        /// Creates an AuthorizationInProgress containing the generated state value, as well as the redirect request.
        /// The returned values may be used to add and delete cookies to check state.
        /// </summary>
        /// <param name="clientId">The client ID of your Spotify App</param>
        /// <param name="appRedirectUri">The redirect url of your Spotify App</param>
        /// <param name="scopes">The stringified list of scopes your app requires access to</param>
        /// <returns></returns>
        public static AuthorizationInProgress GetAuthStateAndRedirect(string clientId, string appRedirectUri, string scopes) {
            string SpotifyAuthRequestUrl = "https://accounts.spotify.com/authorize";
            string stateValue = GetAuthState();
            string redirect = $"{SpotifyAuthRequestUrl}?response_type=code&client_id={clientId}&scope={scopes}&redirect_uri={appRedirectUri}&state={stateValue}";
            return new AuthorizationInProgress(stateValue, redirect);
        }

        /// <summary>
        /// Returns a SpotifyOAuthCredentials. On success, returns with an access token, a refresh token, and a timer limit for the validity of the access token. (Refresh has no expiry). Sets WasSuccessful to True
        /// On error, returns with the error code and message, and the other fields are null. Also sets WasSuccess to false.
        /// </summary>
        /// <param name="OAuthToken"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public static async Task<OAuthCredentials> GetSpotifyTokenCredentials(string OAuthToken, string clientId, string clientSecret, string redirectUri){
            string postBody = $"code={OAuthToken}&redirect_uri={redirectUri}&grant_type=authorization_code";
            return await GetSpotifyCredentials(postBody, clientId, clientSecret);        
        }

        public static async Task<OAuthCredentials> RefreshAccessToken(OAuthCredentials Credentials, string clientId, string clientSecret){
            return await RefreshAccessToken(Credentials.Refresh_token, clientId, clientSecret);
        }

        public static async Task<OAuthCredentials> RefreshAccessToken(string refreshToken, string clientId, string clientSecret){
            string postBody = $"grant_type=refresh_token&refresh_token={refreshToken}";
            return await GetSpotifyCredentials(postBody, clientId, clientSecret, refreshToken);                         
        }

        #region private helpers
        private static async Task<OAuthCredentials> GetSpotifyCredentials(string PostBody, string clientId, string clientSecret, string refreshToken = null){
            string b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            HttpRequestMessage request = WebRequestHelpers.SetupRequest(SpotifyAPITokenUrl, "Basic", b64, HttpMethod.Post);
            HttpContent content = new StringContent(PostBody, Encoding.UTF8, "application/x-www-form-urlencoded");
            request.Content = content;
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(request);
            if(response.IsSuccessStatusCode){
                string jsonstring = await response.Content.ReadAsStringAsync();
                JObject jobj = Newtonsoft.Json.Linq.JObject.Parse(jsonstring);
                string accessToken = jobj.GetValue("access_token").ToString();
                int expiresIn = Convert.ToInt32(jobj.GetValue("expires_in").ToString());
                if(string.IsNullOrWhiteSpace(refreshToken)){
                    refreshToken = jobj.GetValue("refresh_token").ToString();
                }
                return new OAuthCredentials("unknown user", accessToken, expiresIn, refreshToken);
            }
            else {
                return OAuthCredentials.CrendentialError;
            }
        }
        #endregion
    }
}

