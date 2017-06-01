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


        private static async Task<IEnumerable<bool>> DoHttpGetBools(string accessToken, string endpoint) {
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
            else if(t == typeof(Paging<SavedAlbum>)){
                generator = (tk) => { return new Paging<SavedAlbum>(tk); };
            }
            else if (t == typeof(Paging<Artist>)) {
                generator = (tk) => { return new Paging<Artist>(tk); };
            }
            else if(t == typeof(Paging<Track>)) {
                generator = (tk) => { return new Paging<Track>(tk); };
            }
            else if (t == typeof(Paging<SavedTrack>)) {
                generator = (tk) => { return new Paging<SavedTrack>(tk); };
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
                    throw new ArgumentException($"No generator exists for the supplied type: {typeof(T)}");
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
        private static async Task<IEnumerable<T>> DoSeveralHttp<T>(string endpoint, string type, string accessToken){
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
                    throw new ArgumentException($"No generator exists for the supplied type: {typeof(T)}");
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

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="accessToken">OAuth acccess token</param>
        /// <param name="id">Album Id</param>
        /// <param name="market">Market result is desired in</param>
        /// <returns></returns>
        public static async Task<Album> GetAnAlbum(string accessToken, string id, string market = "") {
            string endpoint = $"https://api.spotify.com/v1/albums/{id}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"market",market}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint, id) + options;
            return await DoHTTP<Album>(req, accessToken);
        }

        /// <summary>
        /// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
        /// This endpoint can only take a maximum of 20 ids - excess ids will be ignored
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">List of Album Ids to get</param>
        /// <param name="market">Market results are desired in</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Album>> GetSeveralAlbums(string accessToken, IEnumerable<string> ids, string market = "") {
            int maxparams = 20;
            string endpoint = "https://api.spotify.com/v1/albums/?ids={0}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"market",market}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint, string.Join(",",ids.Take(maxparams))) + options;
            return await DoSeveralHttp<Album>(req, "albums", accessToken);
        }

        /// <summary>
        /// Get Spotify catalog information about an album’s tracks. Optional parameters can be used to limit the number of tracks returned.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">The Spotify ID for the album.</param>
        /// <param name="limit">Optional. The maximum number of tracks to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first track to return. Default: 0 (the first object). Use with limit to get the next set of tracks. </param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">The Spotify ID for the artist.</param>
        /// <returns></returns>
        public static async Task<Artist> GetAnArtist(string accessToken, string id) {
            string endpoint = "https://api.spotify.com/v1/artists/{0}";
            string req = string.Format(endpoint, id);
            return await DoHTTP<Artist>(req, accessToken);
        }

        /// <summary>
        /// Get Spotify catalog information for several artists based on their Spotify IDs.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">Required. A list of the Spotify IDs for the artists. Maximum: 50 IDs.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Artist>> GetSeveralArtists(string accessToken, IEnumerable<string> ids) {
            int maxparams = 50;
            string endpoint = "https://api.spotify.com/v1/artists?ids={0}";
            string req = string.Format(endpoint, string.Join(",",ids.Take(maxparams)));
            return await DoSeveralHttp<Artist>(req, "artists", accessToken);
        }

        /// <summary>
        /// Get Spotify catalog information about an artist’s albums. Optional parameters can be specified in the query string to filter and sort the response.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">The Spotify ID for the artist.</param>
        /// <param name="album_type">Optional. A comma-separated list of keywords that will be used to filter the response. 
        /// If not supplied, all album types will be returned. Valid values are: album, single, appears_on, compilation
        /// </param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Supply this parameter to limit the response to one particular geographical market. For example, for albums available in Sweden: market="SE".
        /// If not given, results will be returned for all markets and you are likely to get duplicate results per album, one for each market in which the album is available!</param>
        /// <param name="limit">Optional. The number of album objects to return. Default: 20. Minimum: 1. Maximum: 50. For example: limit=2</param>
        /// <param name="offset">Optional. The index of the first album to return. Default: 0 (i.e., the first album). Use with limit to get the next set of albums. </param>
        /// <returns></returns>
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

        /// <summary>
        /// Get Spotify catalog information about an artist’s top tracks by country.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">The Spotify ID for the artist.</param>
        /// <param name="country">Required. The country: an ISO 3166-1 alpha-2 country code. </param>
        /// <returns></returns>
        public static async Task<IEnumerable<Track>> GetArtistsTopTracks(string accessToken, string id, string country="US") {
            string endpoint = $"https://api.spotify.com/v1/artists/{id}/top-tracks";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"country", country}
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoSeveralHttp<Track>(req, "tracks", accessToken);
        }

        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. Similarity is based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">The Spotify ID for the artist.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Artist>> GetRelatedArtists(string accessToken, string id) {
            string endpoint = $"https://api.spotify.com/v1/artists/{id}/related-artists";
            return await DoSeveralHttp<Artist>(endpoint, "artists", accessToken);
        }

        /// <summary>
        /// Get a detailed audio analysis for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">Required. A valid access token from the Spotify Accounts service: see the Web API Authorization Guide for details.</param>
        /// <returns></returns>
        public static async Task<AudioAnalysis> GetAudioAnalysis(string accessToken, string id) {
            string endpoint = $"https://api.spotify.com/v1/audio-analysis/{id}";
            return await DoHTTP<AudioAnalysis>(endpoint, accessToken);
        }

        /// <summary>
        /// Get audio feature information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">Required. A valid access token from the Spotify Accounts service: see the Web API Authorization Guide for details.</param>
        /// <returns></returns>
        public static async Task<AudioFeatures> GetAudioFeatures(string accessToken, string id) {
            string endpoint = $"https://api.spotify.com/v1/audio-features/{id}";
            return await DoHTTP<AudioFeatures>(endpoint, accessToken);
        }

        /// <summary>
        /// Get audio features for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">Required. A list of the Spotify IDs for the tracks. Maximum: 100 IDs.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<AudioFeatures>> GetSeveralAudioFeatures(string accessToken, IEnumerable<string> ids) {
            int maxparams = 100;
            string endpoint = "https://api.spotify.com/v1/audio-features/?ids={0}";
            string req = string.Format(endpoint, string.Join(",", ids.Take(maxparams)));
            return await DoSeveralHttp<AudioFeatures>(req, "audio_features", accessToken);
        }

        /// <summary>
        /// Get a list of Spotify featured playlists (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="locale">Optional. The desired language, consisting of a lowercase ISO 639 language code and an uppercase ISO 3166-1 alpha-2 country code, joined by an underscore. 
        /// For example: es_MX, meaning "Spanish (Mexico)". 
        /// Provide this parameter if you want the results returned in a particular language (where available).
        ///  Note that, if locale is not supplied, or if the specified language is not available, all strings will be returned in the Spotify default language (American English).
        /// The locale parameter, combined with the country parameter, may give odd results if not carefully matched. For example country=SE&locale=de_DE will return a list of categories relevant to Sweden but as German language strings.
        /// </param>
        /// <param name="country">Optional. A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want the list of returned items to be relevant to a particular country. If omitted, the returned items will be relevant to all countries.</param>
        /// <param name="timestamp">Optional. A timestamp in ISO 8601 format: yyyy-MM-ddTHH:mm:ss. Use this parameter to specify the user's local time to get results tailored for that specific date and time in the day. If not provided, the response defaults to the current UTC time. Example: "2014-10-23T09:00:00" for a user whose local time is 9AM.</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items. </param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a list of new album releases featured in Spotify (shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="country">Optional. A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want the list of returned items to be relevant to a particular country. If omitted, the returned items will be relevant to all countries.</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items. </param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a list of categories used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="locale">Optional. The desired language, consisting of an ISO 639 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore. For example: es_MX, meaning "Spanish (Mexico)". Provide this parameter if you want the category metadata returned in a particular language.  Note that, if locale is not supplied, or if the specified language is not available, all strings will be returned in the Spotify default language (American English).  The locale parameter, combined with the country parameter, may give odd results if not carefully matched. For example country=SE&locale=de_DE will return a list of categories relevant to Sweden but as German language strings.</param>
        /// <param name="country">Optional. A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want to narrow the list of returned categories to those relevant to a particular country. If omitted, the returned items will be globally relevant.</param>
        /// <param name="limit">Optional. The maximum number of categories to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of categories. </param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a single category used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="category_id">The Spotify category ID for the category.</param>
        /// <param name="country">Optional. A country: an ISO 3166-1 alpha-2 country code. Provide this parameter to ensure that the category exists for a particular country.</param>
        /// <param name="locale">Optional. The desired language, consisting of an ISO 639 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore. For example: es_MX, meaning "Spanish (Mexico)". Provide this parameter if you want the category strings returned in a particular language.   Note that, if locale is not supplied, or if the specified language is not available, the category strings returned will be in the Spotify default language (American English).</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a list of Spotify playlists tagged with a particular category.
        /// </summary>
        /// <param name="accessToken">Oauth access token</param>
        /// <param name="category_id">The Spotify category ID for the category.</param>
        /// <param name="country">Optional. A country: an ISO 3166-1 alpha-2 country code.</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first item to return. Default: 0 (the first object). Use with limit to get the next set of items. </param>
        /// <returns></returns>
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

        /// <summary>
        /// Get detailed profile information about the current user (including the current user’s username).
        /// Reading the user's email address requires the user-read-email scope; reading country and product subscription level requires the user-read-private scope. Reading the user's birthdate requires the user-read-birthdate scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <returns></returns>
        public static async Task<User> GetCurrentUser(string accessToken) {
            string endpoint = "https://api.spotify.com/v1/me";
            return await DoHTTP<User>(endpoint, accessToken);
        }

        /// <summary>
        /// Get the current user’s followed artists.
        /// Getting details of the artists or users the current user follows requires authorization of the user-follow-read scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="after">Optional. The last artist ID retrieved from the previous request.</param>
        /// <returns></returns>
        public static async Task<CursorBasedPaging<Artist>> GetUsersFollowedArtists(string accessToken, int limit = 20, string after = "") {
            string type = "artist";
            string endpoint = $"https://api.spotify.com/v1/me/following";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type},
                {"limit", limit},
                {"after", after}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<CursorBasedPaging<Artist>>(req, accessToken, key:"artists");
        }

        /// <summary>
        /// Add the current user as a follower of one or more artists or other Spotify users.
        /// Modifying the list of artists or users the current user follows requires authorization of the user-follow-modify scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="type">Required. The ID type: either "artist" or "user".</param>
        /// <param name="ids">A list of the artist or the user Spotify IDs. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns></returns>
        public static async Task<RegularError> FollowArtistOrUsers(string accessToken, string type, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/following";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;

            Dictionary<string, object> putItems = new Dictionary<string, object>() {
                {"ids", ids }
            };
            JObject putObject = JObject.FromObject(putItems);

            HttpRequestMessage message = WebRequestHelpers.SetupRequest(req, accessToken, HttpMethod.Put);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully followed some {type}");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Remove the current user as a follower of one or more artists or other Spotify users.
        /// Modifying the list of artists or users the current user follows requires authorization of the user-follow-modify scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="type">Required. The ID type: either "artist" or "user".</param>
        /// <param name="ids">A list of the artist or the user Spotify IDs.</param>
        /// <returns></returns>
        public static async Task<RegularError> UnfollowArtistOrUsers(string accessToken, string type, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/following";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;

            Dictionary<string, object> putItems = new Dictionary<string, object>() {
                {"ids", ids }
            };
            JObject putObject = JObject.FromObject(putItems);

            HttpRequestMessage message = WebRequestHelpers.SetupRequest(req, accessToken, HttpMethod.Delete);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully unfollowed some {type}");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Check to see if the current user is following one or more artists or other Spotify users.
        /// Getting details of the artists or users the current user follows requires authorization of the user-follow-read scope. See Using Scopes.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="type">Required. The ID type: either "artist" or "user".</param>
        /// <param name="ids">Required. A list of the artist or the user Spotify IDs to check.  A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>List of true or false values, in the same order in which the ids were specified. </returns>
        public static async Task<IEnumerable<bool>> CheckCurrentUserFollowsArtistsOrUsers(string accessToken, string type, IEnumerable<string> ids) {
            int maxparams = 50;
            string endpoint = "https://api.spotify.com/v1/me/following/contains";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"type", type},
                {"ids", string.Join(",", ids.Take(maxparams)) }
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            //TODO
            return await DoHttpGetBools(accessToken, req);
        }

        /// <summary>
        /// Add the current user as a follower of a playlist.
        /// Following a playlist publicly requires authorization of the playlist-modify-public scope; following it privately requires the playlist-modify-private scope. See Using Scopes. 
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="owner_id">The Spotify user ID of the person who owns the playlist.</param>
        /// <param name="playlist_id">The Spotify ID of the playlist. Any playlist can be followed, regardless of its public/private status, as long as you know its playlist ID.</param>
        /// <param name="isPublic">Optional, default true. If true the playlist will be included in user's public playlists, if false it will remain private. To be able to follow playlists privately, the user must have granted the playlist-modify-private scope.</param>
        /// <returns></returns>
        public static async Task<RegularError> FollowAPlaylist(string accessToken, string owner_id, string playlist_id, bool isPublic = true) {
            string endpoint = $"https://api.spotify.com/v1/users/{owner_id}/playlists/{playlist_id}/followers";
            Dictionary<string, object> putItems = new Dictionary<string, object>() {
                {"public", isPublic }
            };
            JObject putObject = JObject.FromObject(putItems);

            HttpRequestMessage message = WebRequestHelpers.SetupRequest(endpoint, accessToken, HttpMethod.Put);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully followed playlist {playlist_id}");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Remove the current user as a follower of a playlist.
        ///  Unfollowing a publicly followed playlist for a user requires authorization of the playlist-modify-public scope; unfollowing a privately followed playlist requires the playlist-modify-private scope. See Using Scopes.
        ///  Note that the scopes you provide relate only to whether the current user is following the playlist publicly or privately (i.e. showing others what they are following), not whether the playlist itself is public or private.
        /// </summary>
        /// <param name="accessToken">Oauth access token</param>
        /// <param name="owner_id">The Spotify user ID of the person who owns the playlist.</param>
        /// <param name="playlist_id">The Spotify ID of the playlist that is to be no longer followed.</param>
        /// <returns></returns>
        public static async Task<RegularError> UnfollowAPlaylist(string accessToken, string owner_id, string playlist_id) {
            string endpoint = $"https://api.spotify.com/v1/users/{owner_id}/playlists/{playlist_id}/followers";
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(endpoint, accessToken, HttpMethod.Delete);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully unfollowed playlist {playlist_id}");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Save Tracks for User
        /// Modification of the current user's "Your Music" collection requires authorization of the user-library-modify scope.
        /// Trying to add a track when you do not have the user’s authorization, or when you have over 10.000 tracks in Your Music, returns error 403 Forbidden.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">A list of the Spotify IDs.</param>
        /// <returns></returns>
        public static async Task<RegularError> SaveTracksForUser(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/tracks";
            int maxparams = 50;
            string[] idarr = ids.Take(maxparams).ToArray();
            JArray putObject = JArray.FromObject(idarr);
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(endpoint, accessToken, HttpMethod.Put);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully saved {idarr.Length} tracks");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Get a list of the songs saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects. </param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public static async Task<Paging<SavedTrack>> GetUsersSavedTracks(string accessToken, int limit = 20, int offset = 0, string market = "") {
            string endpoint = "https://api.spotify.com/v1/me/tracks";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"limit", limit},
                {"offset", offset},
                {"market", market}
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<Paging<SavedTrack>>(req, accessToken);
        }

        /// <summary>
        /// Remove one or more tracks from the current user’s “Your Music” library.
        ///  Modification of the current user's "Your Music" collection requires authorization of the user-library-modify scope.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">A list of Spotify Ids</param>
        /// <returns></returns>
        public static async Task<RegularError> RemoveUsersSavedTracks(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/tracks";
            int maxParams = 50;
            string[] idArr = ids.Take(maxParams).ToArray();
            JArray putObject = JArray.FromObject(idArr);
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(endpoint, accessToken, HttpMethod.Delete);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully removed {idArr.Length} tracks from users library");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Check if one or more tracks is already saved in the current Spotify user’s “Your Music” library.
        /// The user-library-read scope must have been authorized by the user.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">List of Spotify Ids</param>
        /// <returns></returns>
        public static async Task<IEnumerable<bool>> CheckUsersSavedTracks(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/tracks/contains";
            int maxParams = 50;
            string[] idArr = ids.Take(maxParams).ToArray();
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"ids", string.Join(",",idArr) }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHttpGetBools(accessToken, req);
        }

        /// <summary>
        /// Save one or more albums to the current user’s “Your Music” library.
        /// Modification of the current user's "Your Music" collection requires authorization of the user-library-modify scope.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">List of Spotify Ids</param>
        /// <returns></returns>
        public static async Task<RegularError> SaveAlbumsForUser(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/albums";
            int maxparams = 50;
            string[] idarr = ids.Take(maxparams).ToArray();
            JArray putObject = JArray.FromObject(idarr);
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(endpoint, accessToken, HttpMethod.Put);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully saved {idarr.Length} albums");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Get a list of the albums saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="accessToken">OAuth token</param>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects. </param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public static async Task<Paging<SavedAlbum>> GetUsersSavedAlbums(string accessToken, int limit = 20, int offset = 0, string market = "") {
            string endpoint = "https://api.spotify.com/v1/me/albums";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"limit", limit},
                {"offset", offset },
                {"market", market},
            };
            string options = EncodeRequestParams(paramDict);
            string req = string.Format(endpoint) + options;
            return await DoHTTP<Paging<SavedAlbum>>(req, accessToken);
        }

        /// <summary>
        /// Remove one or more albums from the current user’s “Your Music” library.
        /// Changes to a user’s saved albums may not be visible in other Spotify applications immediately.
        /// Modification of the current user's "Your Music" collection requires authorization of the user-library-modify scope.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">List of Spotify Ids</param>
        /// <returns></returns>
        public static async Task<RegularError> RemoveAlbumsForCurrentUser(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/albums";
            int maxParams = 50;
            string[] idArr = ids.Take(maxParams).ToArray();
            JArray putObject = JArray.FromObject(idArr);
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(endpoint, accessToken, HttpMethod.Delete);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            message.Content = new StringContent(putObject.ToString());
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                return new RegularError(false, $"successfully removed {idArr.Length} albums from users library");
            }
            else {
                return new RegularError(response.IsSuccessStatusCode, (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        /// <summary>
        /// Check if one or more albums is already saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">List of Spotify Ids</param>
        /// <returns></returns>
        public static async Task<IEnumerable<bool>> CheckUsersSavedAlbums(string accessToken, IEnumerable<string> ids) {
            string endpoint = "https://api.spotify.com/v1/me/albums/contains";
            int maxParams = 50;
            string[] idArr = ids.Take(maxParams).ToArray();
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"ids", string.Join(",",idArr) }
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHttpGetBools(accessToken, req);
        }


        /// <summary>
        /// Get the current user’s top artists or tracks based on calculated affinity.
        /// Affinity is a measure of the expected preference a user has for a particular track or artist. 
        /// It is based on user behavior, including play history, but does not include actions made while in incognito mode. 
        /// Light or infrequent users of Spotify may not have sufficient play history to generate a full affinity data set.
        /// 
        /// As a user’s behavior is likely to shift over time, this preference data is available over three time spans.
        /// See time_range in the query parameter table for more information. 
        /// 
        /// For each time range, the top 50 tracks and artists are available for each user. 
        /// In the future, it is likely that this restriction will be relaxed. 
        /// This data is typically updated once each day for each user.
        /// 
        /// Getting details of a user's top artists and tracks requires authorization of the user-top-read scope. See Using Scopes.
        /// 
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="limit">Optional. The number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first entity to return. Default: 0 (i.e., the first track). Use with limit to get the next set of entities.</param>
        /// <param name="time_range">Optional. Over what time frame the affinities are computed. Valid values: long_term (calculated from several years of data and including all new data as it becomes available), medium_term (approximately last 6 months), short_term (approximately last 4 weeks). Default: medium_term. </param>
        /// <returns></returns>
        public static async Task<Paging<Artist>> GetUsersTopArtists(string accessToken, int limit = 20, int offset=0, string time_range = "medium_term") {
            string type = "artists";
            string endpoint = $"https://api.spotify.com/v1/me/top/{type}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"limit",limit },
                {"offset",offset },
                {"time_range",time_range}
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<Artist>>(req, accessToken);
        }

        /// <summary>
        /// Get the current user’s top artists or tracks based on calculated affinity.
        /// Affinity is a measure of the expected preference a user has for a particular track or artist. 
        /// It is based on user behavior, including play history, but does not include actions made while in incognito mode. 
        /// Light or infrequent users of Spotify may not have sufficient play history to generate a full affinity data set.
        /// 
        /// As a user’s behavior is likely to shift over time, this preference data is available over three time spans.
        /// See time_range in the query parameter table for more information. 
        /// 
        /// For each time range, the top 50 tracks and artists are available for each user. 
        /// In the future, it is likely that this restriction will be relaxed. 
        /// This data is typically updated once each day for each user.
        /// 
        /// Getting details of a user's top artists and tracks requires authorization of the user-top-read scope. See Using Scopes.
        /// 
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="limit">Optional. The number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first entity to return. Default: 0 (i.e., the first track). Use with limit to get the next set of entities.</param>
        /// <param name="time_range">Optional. Over what time frame the affinities are computed. Valid values: long_term (calculated from several years of data and including all new data as it becomes available), medium_term (approximately last 6 months), short_term (approximately last 4 weeks). Default: medium_term. </param>
        /// <returns></returns>
        public static async Task<Paging<Track>> GetUsersTopTracks(string accessToken, int limit = 20, int offset = 0, string time_range = "medium_term") {
            string type = "tracks";
            string endpoint = $"https://api.spotify.com/v1/me/top/{type}";
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"limit",limit },
                {"offset",offset },
                {"time_range",time_range}
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoHTTP<Paging<Track>>(req, accessToken);
        }

        //TODO Recommendations
        //This is complicated
        //    public static async Task<Recommendation> GetRecommendationsBasedOnSeed(string accessToken, AudioFeatures MaxTuneableValues = null, AudioFeatures MinTuneableValues = null, AudioFeatures TargetValues = null,  IEnumerable<string> seed_artists = null, IEnumerable<string> seed_tracks, IEnumerable<string> seed_genres, int limit = 20, string market = "", ) {
        //        if(MaxTuneableValues == null) {
        //            MaxTuneableValues = new AudioFeatures(
        //                    AudioFeatures.Acousticness_Max, AudioFeatures.Danceability_Max, AudioFeatures.Energy_Max, AudioFeatures.Instrumentalness_Max,
        //                    AudioFeatures.Key_Max, AudioFeatures.Liveness_Max, AudioFeatures.Loudness_Max, AudioFeatures.Mode_Max, AudioFeatures.Speechiness_Max, 
        //                    AudioFeatures.Tempo_Max, AudioFeatures.Time_Signature_Max, AudioFeatures.Valence_Max,
        //                    "", int.MaxValue, "", "", "");
        //        }
        //        if (MinTuneableValues == null) {
        //            MinTuneableValues = new AudioFeatures(
        //                    AudioFeatures.Acousticness_Min, AudioFeatures.Danceability_Min, AudioFeatures.Energy_Min, AudioFeatures.Instrumentalness_Min,
        //                    AudioFeatures.Key_Min, AudioFeatures.Liveness_Min, AudioFeatures.Loudness_Min, AudioFeatures.Mode_Min, AudioFeatures.Speechiness_Min,
        //                    AudioFeatures.Tempo_Min, AudioFeatures.Time_Signature_Min, AudioFeatures.Valence_Min,
        //                    "", int.MaxValue, "", "", "");
        //        
        //    }

        //TODO Search
        //Also complicated


        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="id">The Spotify ID for the track.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public static async Task<Track> GetATrack(string accessToken, string id, string market = "") {
                string endpoint = $"https://api.spotify.com/v1/tracks/{id}";
                Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                    {"market",market },
                };
                string options = EncodeRequestParams(paramDict);
                string req = endpoint + options;
                return await DoHTTP<Track>(req, accessToken);
            }

        /// <summary>
        /// Get Spotify catalog information for multiple tracks based on their Spotify IDs.
        /// 
        /// Objects are returned in the order requested. If an object is not found, a null value is returned in the appropriate position. 
        /// Duplicate ids in the query will result in duplicate objects in the response. 
        /// </summary>
        /// <param name="accessToken">OAuth access token</param>
        /// <param name="ids">A List of Spotify Ids</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Track>> GetSeveralTracks(string accessToken, IEnumerable<string> ids, string market = "") {
            string type = "tracks";
            string endpoint = "https://api.spotify.com/v1/tracks";
            int maxParams = 50;
            string[] idArr = ids.Take(maxParams).ToArray();
            Dictionary<string, object> paramDict = new Dictionary<string, object>() {
                {"ids", string.Join(",", idArr)},
                {"market",market},
            };
            string options = EncodeRequestParams(paramDict);
            string req = endpoint + options;
            return await DoSeveralHttp<Track>(req, type, accessToken);
        }

    }

}
