using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DotNetStandardSpotifyWebApi.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DotNetStandardSpotifyWebApi.Authorization;

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


        internal static Func<JToken, ISpotifyObject> CreateSpotifyObjectGenerator(Type t) {
            if (t == typeof(User)) {
                return (tk) => { return new User(tk); };
            }
            else if (t == typeof(Playlist)) {
                return (tk) => { return new Playlist(tk); };
            }
            else if (t == typeof(PlaylistTrack)) {
                return (tk) => { return new PlaylistTrack(tk); };
            }
            else if (t == typeof(Track)) {
                return (tk) => { return new Track(tk); };
            }
            else if (t == typeof(SavedTrack)) {
                return (tk) => { return new SavedTrack(tk); };
            }
            else if (t == typeof(Artist)) {
                return (tk) => { return new Artist(tk); };
            }
            else if (t == typeof(Album)) {
                return (tk) => { return new Album(tk); };
            }
            else if (t == typeof(SavedAlbum)) {
                return (tk) => { return new SavedAlbum(tk); };
            }
            else if (t == typeof(Category)) {
                return (tk) => { return new Category(tk); };
            }
            else if (t == typeof(AudioFeatures)) {
                return (tk) => { return new AudioFeatures(tk); };
            }
            else if (t == typeof(AudioAnalysis)) {
                return (tk) => { return new AudioAnalysis(tk); };
            }
            else if (t == typeof(Context)) {
                return (tk) => { return new Context(tk); };
            }
            else if (t == typeof(PlayHistory)) {
                return (tk) => { return new PlayHistory(tk); };
            }
            else if (t == typeof(Device)) {
                return (tk) => { return new Device(tk); };
            }
            else if (t == typeof(CurrentlyPlayingContext)) {
                return (tk) => { return new CurrentlyPlayingContext(tk); };
            }
            else if (t == typeof(Paging<Playlist>)) {
                return (tk) => { return new Paging<Playlist>(tk); };
            }
            else if (t == typeof(Paging<PlaylistTrack>)) {
                return (tk) => { return new Paging<PlaylistTrack>(tk); };
            }
            else if (t == typeof(Paging<Album>)) {
                return (tk) => { return new Paging<Album>(tk); };
            }
            else if (t == typeof(Paging<SavedAlbum>)) {
                return (tk) => { return new Paging<SavedAlbum>(tk); };
            }
            else if (t == typeof(Paging<Artist>)) {
                return (tk) => { return new Paging<Artist>(tk); };
            }
            else if (t == typeof(Paging<Track>)) {
                return (tk) => { return new Paging<Track>(tk); };
            }
            else if (t == typeof(Paging<SavedTrack>)) {
                return (tk) => { return new Paging<SavedTrack>(tk); };
            }
            else if (t == typeof(Paging<Category>)) {
                return (tk) => { return new Paging<Category>(tk); };
            }
            else if (t == typeof(Paging<Context>)) {
                return (tk) => { return new Paging<Context>(tk); };
            }
            else if (t == typeof(Paging<PlayHistory>)) {
                return (tk) => { return new Paging<PlayHistory>(tk); };
            }
            else if (t == typeof(Paging<Device>)) {
                return (tk) => { return new Paging<Device>(tk); };
            }
            else if (t == typeof(CursorBasedPaging<Artist>)) {
                return (tk) => { return new CursorBasedPaging<Artist>(tk); };
            }
            else if (t == typeof(CursorBasedPaging<Album>)) {
                return (tk) => { return new CursorBasedPaging<Album>(tk); };
            }
            else if (t == typeof(CursorBasedPaging<SavedAlbum>)) {
                return (tk) => { return new CursorBasedPaging<SavedAlbum>(tk); };
            }
            else if (t == typeof(CursorBasedPaging<Track>)) {
                return (tk) => { return new CursorBasedPaging<Track>(tk); };
            }
            else if (t == typeof(CursorBasedPaging<SavedTrack>)) {
                return (tk) => { return new CursorBasedPaging<SavedTrack>(tk); };
            }
            else if (t == typeof(CursorBasedPaging<Playlist>)) {
                return (tk) => { return new CursorBasedPaging<Playlist>(tk); };
            }
            else if (t == typeof(CursorBasedPaging<PlaylistTrack>)) {
                return (tk) => { return new CursorBasedPaging<PlaylistTrack>(tk); };
            }
            else if (t == typeof(CursorBasedPaging<Context>)) {
                return (tk) => { return new CursorBasedPaging<Context>(tk); };
            }
            else if (t == typeof(CursorBasedPaging<PlayHistory>)) {
                return (tk) => { return new CursorBasedPaging<PlayHistory>(tk); };
            }
            else if (t == typeof(CursorBasedPaging<Device>)) {
                return (tk) => { return new CursorBasedPaging<Device>(tk); };
            }
            else if (t == typeof(FeaturedPlaylists)) {
                return (tk) => { return new FeaturedPlaylists(tk); };
            }
            else if(t == typeof(SpotifyList<string>)) {
                return (tk) => { return new SpotifyList<string>(tk); };
            }
            else if (t == typeof(SpotifyList<bool>)) {
                return (tk) => { return new SpotifyList<bool>(tk); };
            }
            else if (t == typeof(SpotifyList<int>)) {
                return (tk) => { return new SpotifyList<int>(tk); };
            }
            else {
                return (tk) => {
                    throw new ArgumentException($"No generator exists for the supplied type: {t}");
                };
            }
        }
        internal static async Task<WebResult<IReadOnlyList<bool>>> DoHttpGetBools(string accessToken, string endpoint, EntityTagHeaderValue etag = null) {
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(endpoint, accessToken);
            if (etag != null) {
                message.Headers.IfNoneMatch.Add(etag);
            }
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                JArray jarr = token.Value<JArray>();
                List<bool> lst = new List<bool>();
                foreach (JValue jobj in jarr) {
                    bool contains = jobj.Value<bool>();
                    lst.Add(contains);
                }
                return new WebResult<IReadOnlyList<bool>>(response.IsSuccessStatusCode, response.StatusCode, response.ReasonPhrase, response.Headers.ETag, lst);
            }
            else {
                return new WebResult<IReadOnlyList<bool>>(response.IsSuccessStatusCode, response.StatusCode, response.ReasonPhrase, response.Headers.ETag,  null);
            }

        }
        internal static async Task<WebResult<T>> DoHTTP<T>(string endpoint, string accessToken, EntityTagHeaderValue etag = null, string key = "") where T : ISpotifyObject {

            Func<JToken, ISpotifyObject> generator = CreateSpotifyObjectGenerator(typeof(T));
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(endpoint, accessToken);
            if(etag != null) {
                message.Headers.IfNoneMatch.Add(etag);
            }
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                if (!string.IsNullOrWhiteSpace(key)) {
                    token = token.Value<JToken>(key);
                }
                T item = (T)generator(token);
                return new WebResult<T>(response.IsSuccessStatusCode, response.StatusCode, response.ReasonPhrase, response.Headers.ETag, item);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest) {
                JObject jobj = JObject.Parse(response.Content.ToString());
                string revoked = jobj.Value<string>("error");
                if (!string.IsNullOrWhiteSpace(revoked)) {
                    throw new TokenRevokedException();
                }
            }
            else if ((int)response.StatusCode == 429) {
                throw new RateLimitException(response.Headers.RetryAfter.Delta.Value.TotalSeconds);
            }
            return new WebResult<T>(response.IsSuccessStatusCode, response.StatusCode, response.ReasonPhrase, response.Headers.ETag,  default(T));
        }
        internal static async Task<WebResult<IEnumerable<T>>> DoSeveralHttp<T>(string endpoint, string type, string accessToken, EntityTagHeaderValue etag = null) {
            Func<JToken, ISpotifyObject> generator = CreateSpotifyObjectGenerator(typeof(T));
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(endpoint, accessToken);
            if (etag != null) {
                message.Headers.IfNoneMatch.Add(etag);
            }
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                JArray jarr = token.Value<JArray>(type);
                List<T> lst = new List<T>();
                foreach (JToken Jt in jarr) {
                    if (Jt != null && Jt.Type == JTokenType.Object) {
                        JObject jobj = Jt as JObject;
                        T item = (T)generator(jobj);
                        lst.Add(item);
                    }
                    else {
                        lst.Add(default(T));
                    }
                }
                return new WebResult<IEnumerable<T>>(response.IsSuccessStatusCode, response.StatusCode, response.ReasonPhrase, response.Headers.ETag, lst);

            }
            else if (response.StatusCode == HttpStatusCode.BadRequest) {
                JObject jobj = JObject.Parse(response.Content.ToString());
                string revoked = jobj.Value<string>("error");
                if (!string.IsNullOrWhiteSpace(revoked)) {
                    throw new TokenRevokedException();
                }
            }
            else if ((int)response.StatusCode == 429) {
                throw new RateLimitException(response.Headers.RetryAfter.Delta.Value.TotalSeconds);
            }
            return new WebResult<IEnumerable<T>>(response.IsSuccessStatusCode, response.StatusCode, response.ReasonPhrase, response.Headers.ETag, null);
        }
        //TODO this is nasty
        internal static string EncodeRequestParams(Dictionary<string, object> reqParams) {
            List<string> KeysToRemove = new List<string>();
            foreach (KeyValuePair<string, object> kvp in reqParams) {
                string val = kvp.Value.ToString();
                if (string.IsNullOrEmpty(val)) {
                    KeysToRemove.Add(kvp.Key);
                }
            }
            foreach (string key in KeysToRemove) {
                reqParams.Remove(key);
            }

            string req = "";
            int reqCount = reqParams.Count;
            if (reqCount == 1) {
                KeyValuePair<string, object> First = reqParams.FirstOrDefault();
                string val = First.Value.ToString();
                if (!string.IsNullOrWhiteSpace(val)) {
                    req += $"?{First.Key}={val}";
                }
            }
            else if (reqCount > 1) {
                KeyValuePair<string, object> First = reqParams.FirstOrDefault();

                string val = First.Value.ToString();
                if (!string.IsNullOrWhiteSpace(val)) {
                    req += $"?{First.Key}={First.Value.ToString()}";
                }
                reqParams.Remove(First.Key);
                foreach (KeyValuePair<string, object> kvp in reqParams) {
                    val = kvp.Value.ToString();
                    if (!string.IsNullOrWhiteSpace(val)) {
                        req += $"&{kvp.Key}={kvp.Value.ToString()}";
                    }
                }
            }
            return req;
        }

        internal static async Task<WebResult<bool>> DoMethod(string endpoint, string accessToken, string onSuccess, HttpMethod method, Dictionary<string, object> messageBody = null, EntityTagHeaderValue etag = null) {
            HttpRequestMessage message = SetupRequest(endpoint, accessToken, method);
            if (etag != null) {
                message.Headers.IfNoneMatch.Add(etag);
            }
            if (messageBody != null && messageBody.Any()) {
                JObject putObject = JObject.FromObject(messageBody);
                message.Headers.Accept.Clear();
                message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                message.Content = new StringContent(putObject.ToString());
            }
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new WebResult<bool>(response.IsSuccessStatusCode, response.StatusCode, response.ReasonPhrase, response.Headers.ETag, true);
            }
            else {
                return new WebResult<bool>(response.IsSuccessStatusCode, response.StatusCode, response.ReasonPhrase, response.Headers.ETag, false);
            }
        }



    }
}
