using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DotNetStandardSpotifyWebApi.ObjectModel;
using System.Collections.Generic;
using System.Linq;

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
            else if (t == typeof(Playback)) {
                return (tk) => { return new Playback(tk); };
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
            else if (t == typeof(Paging<Playback>)) {
                return (tk) => { return new Paging<Playback>(tk); };
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
            else if (t == typeof(CursorBasedPaging<Playback>)) {
                return (tk) => { return new CursorBasedPaging<Playback>(tk); };
            }
            else if (t == typeof(CursorBasedPaging<Device>)) {
                return (tk) => { return new CursorBasedPaging<Device>(tk); };
            }
            else if (t == typeof(FeaturedPlaylists)) {
                return (tk) => { return new FeaturedPlaylists(tk); };
            }
            else {
                return (tk) => {
                    throw new ArgumentException($"No generator exists for the supplied type: {t}");
                };
            }
        }
        internal static async Task<IReadOnlyList<bool>> DoHttpGetBools(string accessToken, string endpoint) {
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(endpoint, accessToken);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                JArray jarr = token.Value<JArray>();
                List<bool> lst = new List<bool>();
                foreach (JValue jobj in jarr) {
                    bool contains = jobj.Value<bool>();
                    lst.Add(contains);
                }
                return lst;
            }
            else {
                throw new HttpRequestException($"Status Code: {response.StatusCode}, Message: {response.ReasonPhrase}");
            }

        }
        internal static async Task<T> DoHTTP<T>(string endpoint, string accessToken, string key = "") where T : ISpotifyObject {

            Func<JToken, ISpotifyObject> generator = CreateSpotifyObjectGenerator(typeof(T));
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(endpoint, accessToken);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                if (!string.IsNullOrWhiteSpace(key)) {
                    token = token.Value<JToken>(key);
                }
                T item = (T)generator(token);
                return item;
            }
            else {
                return (T)generator("Error"); //TODO this is broken, yo
            }
        }
        internal static async Task<IEnumerable<T>> DoSeveralHttp<T>(string endpoint, string type, string accessToken) {
            Func<JToken, ISpotifyObject> generator = CreateSpotifyObjectGenerator(typeof(T));
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(endpoint, accessToken);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                JArray jarr = token.Value<JArray>(type);

                List<T> lst = new List<T>();
                foreach (JObject jobj in jarr) {
                    T item = (T)generator(jobj);
                    lst.Add(item);
                }
                return lst;
            }
            else {
                throw new HttpRequestException(response.ReasonPhrase);
            }
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

        internal static async Task<RegularError> DoMethod(string endpoint, string accessToken, string onSuccess, HttpMethod method, Dictionary<string, object> messageBody = null) {
            HttpRequestMessage message = SetupRequest(endpoint, accessToken, method);
            if (messageBody != null && messageBody.Any()) {
                JObject putObject = JObject.FromObject(messageBody);
                message.Headers.Accept.Clear();
                message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                message.Content = new StringContent(putObject.ToString());
            }
            HttpResponseMessage response = await Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, onSuccess);
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }



    }
}
