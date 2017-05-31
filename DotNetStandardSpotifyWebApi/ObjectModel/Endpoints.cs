using System;
using System.Collections.Generic;
using System.Text;
using DotNetStandardSpotifyWebApi.Helpers;
using DotNetStandardSpotifyWebApi.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace DotNetStandardSpotifyWebApi.ObjectModel
{
    public static class Endpoints {


        private static async Task<T> DoHTTP<T>(string endpoint, string accessToken, string key = "") where T : ISpotifyObject {

            Func<JToken, ISpotifyObject> generator;
            Type t = typeof(T);
            if(t == typeof(User)) {
                generator = (tk) => { return new User(tk); };
            }
            else if (t == typeof(Playlist)) {
                generator = (tk) => { return new Playlist(tk); };
            }
            else if (t == typeof(PlaylistTrack)) {
                generator = (tk) => { return new PlaylistTrack(tk); };
            }
            else if (t == typeof(Track)) {
                generator = (tk) => { return new Track(tk); };
            }
            else if (t == typeof(SavedTrack)) {
                generator = (tk) => { return new SavedTrack(tk); };
            }
            else if (t == typeof(Artist)) {
                generator = (tk) => { return new Artist(tk); };
            }
            else if (t == typeof(Album)) {
                generator = (tk) => { return new Album(tk); };
            }
            else if (t == typeof(SavedAlbum)) {
                generator = (tk) => { return new SavedAlbum(tk); };
            }
            else if (t == typeof(Category)) {
                generator = (tk) => { return new Category(tk); };
            }
            else if (t == typeof(AudioFeatures)) {
                generator = (tk) => { return new AudioFeatures(tk); };
            }
            else if (t == typeof(AudioAnalysis)) {
                generator = (tk) => { return new AudioAnalysis(tk); };
            }
            else if (t == typeof(Paging<Playlist>)) {
                generator = (tk) => { return new Paging<Playlist>(tk); };
            }
            else if (t == typeof(Paging<PlaylistTrack>)) {
                generator = (tk) => { return new Paging<PlaylistTrack>(tk); };
            }
            else if (t == typeof(Paging<Album>)) {
                generator = (tk) => { return new Paging<Album>(tk); };
            }
            else if (t == typeof(Paging<Artist>)) {
                generator = (tk) => { return new Paging<Artist>(tk); };
            }
            else if(t == typeof(Paging<Track>)) {
                generator = (tk) => { return new Paging<Track>(tk); };
            }
            else if(t == typeof(Paging<Category>)) {
                generator = (tk) => { return new Paging<Category>(tk); };
            }
            else if (t == typeof(CursorBasedPaging<Artist>)) {
                generator = (tk) => { return new CursorBasedPaging<Artist>(tk); };
            }
            else if (t == typeof(FeaturedPlaylists)) {
                generator = (tk) => { return new FeaturedPlaylists(tk); };
            }
            else {
                generator = (tk) => {
                    throw new ArgumentException("Something happened while generating the paging item!");
                };
            }

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

        private static async Task<IList<T>> DoSeveralHttp<T>(string endpoint, string type, string accessToken){
            Func<JToken, ISpotifyObject> generator;
            Type t = typeof(T);
            if (t == typeof(Playlist)) {
                generator = (tk) => { return new Playlist(tk); };
            }
            else if (t == typeof(PlaylistTrack)) {
                generator = (tk) => { return new PlaylistTrack(tk); };
            }
            else if (t == typeof(Track)) {
                generator = (tk) => { return new Track(tk); };
            }
            else if (t == typeof(SavedTrack)) {
                generator = (tk) => { return new SavedTrack(tk); };
            }
            else if (t == typeof(Artist)) {
                generator = (tk) => { return new Artist(tk); };
            }
            else if (t == typeof(Album)) {
                generator = (tk) => { return new Album(tk); };
            }
            else if (t == typeof(SavedAlbum)) {
                generator = (tk) => { return new SavedAlbum(tk); };
            }
            else if (t == typeof(Category)) {
                generator = (tk) => { return new Category(tk); };
            }
            else if (t == typeof(AudioFeatures)) {
                generator = (tk) => { return new AudioFeatures(tk); };
            }
            else if (t == typeof(AudioAnalysis)) {
                generator = (tk) => { return new AudioAnalysis(tk); };
            }
            else if (t == typeof(Paging<Playlist>)) {
                generator = (tk) => { return new Paging<Playlist>(tk); };
            }
            else if (t == typeof(Paging<PlaylistTrack>)) {
                generator = (tk) => { return new Paging<PlaylistTrack>(tk); };
            }
            else if (t == typeof(Paging<Album>)) {
                generator = (tk) => { return new Paging<Album>(tk); };
            }
            else if (t == typeof(Paging<Artist>)) {
                generator = (tk) => { return new Paging<Artist>(tk); };
            }
            else {
                generator = (tk) => {
                    throw new ArgumentException("Something happened while generating the paging item!");
                };
            }


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
        private static string EncodeRequestParams(Dictionary<string, object> reqParams) {
            List<string> KeysToRemove = new List<string>();
            foreach(KeyValuePair<string,object> kvp in reqParams) {
                string val = kvp.Value.ToString();
                if (string.IsNullOrEmpty(val)) {
                    KeysToRemove.Add(kvp.Key);
                }
            }
            foreach(string key in KeysToRemove) {
                reqParams.Remove(key);
            }

            string req = "";
            int reqCount = reqParams.Count;
            if(reqCount == 1) {
                KeyValuePair<string, object> First = reqParams.FirstOrDefault();
                string val = First.Value.ToString();
                if (!string.IsNullOrWhiteSpace(val)) {
                    req += $"?{First.Key}={val}";
                }
            }
            else if(reqCount > 1){
                KeyValuePair<string, object> First = reqParams.FirstOrDefault();

                string val = First.Value.ToString();
                if (!string.IsNullOrWhiteSpace(val)) {
                    req += $"?{First.Key}={First.Value.ToString()}";
                }
                reqParams.Remove(First.Key);
                foreach (KeyValuePair<string,object> kvp in reqParams) {
                    val = kvp.Value.ToString();
                    if (!string.IsNullOrWhiteSpace(val)) {
                        req += $"&{kvp.Key}={kvp.Value.ToString()}";
                    }
                }
            }
            return req;
        }

        //Get An Album
        public static async Task<Album> GetAnAlbum(string accessToken, string id, string market = "") {
            string endpoint = $"https://api.spotify.com/v1/albums/{id}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"market",market}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint, id) + options;
            return await DoHTTP<Album>(req, accessToken);
        }

        ///Get Several Albums
        public static async Task<IList<Album>> GetSeveralAlbums(string accessToken, IEnumerable<string> ids, string market = "") {
            int maxparams = 20;
            string endpoint = "https://api.spotify.com/v1/albums/?ids={0}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"market",market}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint, string.Join(",",ids.Take(maxparams))) + options;
            return await DoSeveralHttp<Album>(req, "albums", accessToken);
        }

        //Get An Albums Tracks
        public static async Task<Paging<Track>> GetAnAlbumsTracks(string accessToken, string id, int limit = 20, int offset = 0, string market = "") {
            string endpoint = "https://api.spotify.com/v1/albums/{0}/tracks";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"market", market},
                {"limit", limit},
                {"offset", offset}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint, id) + options;
            return await DoHTTP<Paging<Track>>(req, accessToken);
        }

        //Get An Artist
        public static async Task<Artist> GetAnArtist(string accessToken, string id) {
            string endpoint = "https://api.spotify.com/v1/artists/{0}";
            string req = string.Format(endpoint, id);
            return await DoHTTP<Artist>(req, accessToken);
        }

        //Get Several Artists
        public static async Task<IList<Artist>> GetSeveralArtists(string accessToken, IEnumerable<string> ids) {
            int maxparams = 50;
            string endpoint = "https://api.spotify.com/v1/artists?ids={0}";
            string req = string.Format(endpoint, string.Join(",",ids.Take(maxparams)));
            return await DoSeveralHttp<Artist>(req, "artists", accessToken);
        }

        //Get An Artists Albums
        public static async Task<Paging<Album>> GetArtistsAlbums(string accessToken, string id, string album_type = "", string market = "", int limit = 20, int offset = 0) {
            string endpoint = $"https://api.spotify.com/v1/artists/{id}/albums";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"market", market},
                {"limit", limit},
                {"offset", offset},
                {"album_type", album_type }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<Album>>(req, accessToken);
        }

        //Get An Artists Top Tracks
        public static async Task<IList<Track>> GetArtistsTopTracks(string accessToken, string id, string country="US") {
            string endpoint = $"https://api.spotify.com/v1/artists/{id}/top-tracks";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country}
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoSeveralHttp<Track>(req, "tracks", accessToken);
        }

        //Get Related Artists
        public static async Task<IList<Artist>> GetRelatedArtists(string accessToken, string id) {
            string endpoint = $"https://api.spotify.com/v1/artists/{id}/related-artists";
            return await DoSeveralHttp<Artist>(endpoint, "artists", accessToken);
        }

        //Get Audio Analysis for Track
        public static async Task<AudioAnalysis> GetAudioAnalysis(string accessToken, string id) {
            string endpoint = $"https://api.spotify.com/v1/audio-analysis/{id}";
            return await DoHTTP<AudioAnalysis>(endpoint, accessToken);
        }

        //Get Audio Features for a Track
        public static async Task<AudioFeatures> GetAudioFeatures(string accessToken, string id) {
            string endpoint = $"https://api.spotify.com/v1/audio-features/{id}";
            return await DoHTTP<AudioFeatures>(endpoint, accessToken);
        }

        //Get Several Audio Features
        public static async Task<IList<AudioFeatures>> GetSeveralAudioFeatures(string accessToken, IEnumerable<string> ids) {
            int maxparams = 100;
            string endpoint = "https://api.spotify.com/v1/audio-features/?ids={0}";
            string req = string.Format(endpoint, string.Join(",", ids.Take(maxparams)));
            return await DoSeveralHttp<AudioFeatures>(req, "audio_features", accessToken);
        }

        //Get Featured Playlists
        public static async Task<FeaturedPlaylists> GetFeaturedPlaylists(string accessToken, string locale = "", string country = "", string timestamp = "", int limit = 20, int offset = 0) {
            string endpoint = "https://api.spotify.com/v1/browse/featured-playlists";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country},
                {"locale", locale },
                {"timestamp", timestamp },
                {"limit",limit },
                {"offset", offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<FeaturedPlaylists>(req, accessToken);
        }

        //Get New Releases
        public static async Task<Paging<Album>> GetNewReleases(string accessToken, string country = "", int limit = 20, int offset = 0) {
            string endpoint = "https://api.spotify.com/v1/browse/new-releases";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country},
                {"limit",limit },
                {"offset", offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<Paging<Album>>(req, accessToken, key:"albums");
        }

        //Get Categories
        public static async Task<Paging<Category>> GetCategories(string accessToken, string locale = "", string country = "", int limit = 20, int offset = 0) {
            string endpoint = "https://api.spotify.com/v1/browse/categories";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country},
                {"locale", locale },
                {"limit",limit },
                {"offset", offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<Paging<Category>>(req, accessToken, key:"categories");
        }

        //Get A Category
        public static async Task<Category> GetACategory(string accessToken, string category_id, string country = "", string locale = "") {
            string endpoint = $"https://api.spotify.com/v1/browse/categories/{category_id}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country},
                {"locale", locale }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<Category>(req, accessToken);
        }

        //Get a Category's Playlists
        public static async Task<Paging<Playlist>> GetACategorysPlaylists(string accessToken, string category_id, string country = "", int limit = 20, int offset = 0) {
            string endpoint = $"https://api.spotify.com/v1/browse/categories/{category_id}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country},
                {"limit", limit },
                {"offset", offset }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<Paging<Playlist>>(req, accessToken);
        }
        
        ///Get the Current User (aka, as represented by the access token)
        public static async Task<User> GetCurrentUser(string accessToken) {
            string endpoint = "https://api.spotify.com/v1/me";
            return await DoHTTP<User>(endpoint, accessToken);
        }

        //Get the users Followed Artists
        //TODO UNTESTED
        public static async Task<CursorBasedPaging<Artist>> GetUsersFollowedArtists(string accessToken, int limit = 20, string after = "") {
            string type = "artist";
            string endpoint = $"https://api.spotify.com/v1/me/following";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type},
                {"limit", limit },
                {"after", after}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<CursorBasedPaging<Artist>>(req, accessToken, key:"artists");
        }

        //Follow Artists or Users, as specified by the type. Please ensure that all ids in ids are for the same type of Spotify Object
        //that is, do not mix artist or users. For that, please issue a second request of only artists or users
        //TODO UNTESTED
        public static async Task<RegularError> FollowerArtistOrUser(string accessToken, string type, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/following";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;

            Dictionary<string, object> putItems = new Dictionary<string, object>() {
                {"ids", ids }
            };
            JObject putObject = JObject.FromObject(ids);

            HttpRequestMessage message = WebRequestHelpers.SetupRequest(req, accessToken, HttpMethod.Put);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully followed {type}");
            }
            else {
                return new RegularError((int)response.StatusCode, response.ReasonPhrase);
            }
        }
        
    }
}
