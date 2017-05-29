using System.Collections.Generic;
using System.Text;
using DotNetStandardSpotifyWebApi.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace DotNetStandardSpotifyWebApi.ObjectModel {

    /// <summary>
    /// User representing both the Full and Simplified User Objects
    /// https://developer.spotify.com/web-api/object-model/#user-object-public
    /// https://developer.spotify.com/web-api/object-model/#user-object-private
    /// </summary>
    public class User : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// URL locations of the APIs available for getting User objects
        /// </summary>
        private const string api_CurrentProfile = baseUrl + "/v1/me";
        private const string api_FollowedArtists = baseUrl + "/v1/me/following";
        private const string api_FollowingArtistsContains = baseUrl + "/v1/me/following/contains";
        private const string api_SaveTracksToLibrary = baseUrl + "/v1/me/tracks?ids={0}";
        private const string api_GetSavedTracks = baseUrl + "/v1/me/tracks";
        private const string api_CheckSavedTracks = baseUrl + "/v1/me/tracks/contains?ids={0}";
        private const string api_SaveAlbums = baseUrl + "/v1/me/albums?ids={0}";
        private const string api_GetSavedAlbums = baseUrl + "/v1/me/albums";
        private const string api_CheckSavedAlbums = baseUrl + "/v1/me/albums/contains?ids={0}";
        private const string api_GetTop = baseUrl + "/v1/me/top/{0}"; //either 'artists' or 'tracks'
        private const string api_GetPublicProfile = baseUrl + "/v1/users/{0}";
        private const string api_GetPlaylists = baseUrl + "/v1/me/playlists?limit={0}&offset={1}";
        private const string api_GetRecentlyPlayedTracks = baseUrl + "/v1/me/player/recently-played";

        /// <summary>
        /// The user's date-of-birth.
        /// This field is only available when the current user has granted access to the USER_READ_BIRTHDATE scope.
        /// </summary>
        public string Birthdate { get; } = string.Empty;

        /// <summary>
        /// The country of the user, as set in the user's account profile.
        /// An ISO 3166-1 alpha-2 country code. 
        /// This field is only available when the current user has granted access to the USER_READ_PRIVATE scope.
        /// </summary>
        public string Country { get; } = string.Empty;

        /// <summary>
        /// The name displayed on the user's profile. null if not available.
        /// </summary>
        public string DisplayName { get; } = string.Empty;

        /// <summary>
        /// The user's email address, as entered by the user when creating their account.
        /// Important! This email address is unverified; there is no proof that it actually belongs to the user.
        /// This field is only available when the current user has granted access to the USER_READ_EMAIL scope.
        /// </summary>
        public string Email { get; } = string.Empty;

        /// <summary>
        /// Information about the followers of the user. 
        /// </summary>
        public Followers Followers { get; }

        /// <summary>
        /// Known external URLs for this user.
        /// </summary>
        public External_Url[] External_urls { get; } = new External_Url[0];

        /// <summary>
        /// A link to the Web API endpoint for this user.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The Spotify user ID for the user. 
        /// </summary>
        public string Id { get; } = string.Empty;

        /// <summary>
        /// The user's profile image. 
        /// </summary>
        public Image[] Images { get; } = new Image[0];

        /// <summary>
        /// The user's Spotify subscription level: "premium", "free", etc. (The subscription level "open" can be considered the same as "free".) 
        /// This field is only available when the current user has granted access to the USER_READ_PRIVATE scope.
        /// </summary>
        public string Product { get; } = string.Empty;

        /// <summary>
        /// The object type: "user" 
        /// </summary>
        public string Type { get; } = "user";

        /// <summary>
        /// The Spotify URI for the user.
        /// </summary>
        public string Uri { get; } = string.Empty;

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        internal User(JToken token) {
            /* Simple fields */
            Birthdate = token.Value<string>("birthdate") ?? string.Empty;
            Country = token.Value<string>("country") ?? string.Empty;
            DisplayName = token.Value<string>("display_name") ?? string.Empty;
            Email = token.Value<string>("email") ?? string.Empty;
            Product = token.Value<string>("product") ?? string.Empty;
            Href = token.Value<string>("href") ?? string.Empty;
            Id = token.Value<string>("id") ?? string.Empty;
            Uri = token.Value<string>("uri") ?? string.Empty;

            /* External Urls*/
            JObject exturls = token.Value<JObject>("external_urls");
            if(exturls != null) {
                External_Url.FromJObject(token.Value<JObject>("external_urls"));
            }

            /* Followers */
            JToken follower = token.Value<JToken>("followers");
            if(follower != null) {
                Followers = new Followers(follower);
            }

            /* Images */
            JArray images = token.Value<JArray>("images");
            if (images != null) {
                List<Image> ims = new List<Image>();
                foreach (JObject jobj in images.Values<JObject>()) {
                    Image i = new Image(jobj);
                    ims.Add(i);
                }
                Images = ims.ToArray();
            }

        }

        /// <summary>
        /// Error User, given an error message and an error flag
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        internal User(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Blank constructor using defaults
        /// </summary>
        internal User() {

        }

        /// <summary>
        /// Gets the user associated with the given accessToken
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>User object with all information available to the access token, or an error object</returns>
        public static async Task<User> GetCurrentUser(string accessToken) {
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(api_CurrentProfile, accessToken);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);

            User user;
            if (response.IsSuccessStatusCode) {
                JToken j = await WebRequestHelpers.ParseJsonResponse(response.Content);
                user = new User(j);
            }
            else {
                user = new User(true, response.ReasonPhrase);
            }
            return user;
        }

        /// <summary>
        /// Gets the public user information associated with the given user_id
        /// </summary>
        /// <param name="user_id">The id of the Spotify User to get</param>
        /// <param name="accessToken">The OAuth Access token that allows the request to be made</param>
        /// <returns>User object with only public information, or an error object</returns>
        public static async Task<User> GetUser(string user_id, string accessToken) {
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(string.Format(api_GetPublicProfile, user_id), accessToken);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);

            User user;
            if (response.IsSuccessStatusCode) {
                JToken j = await WebRequestHelpers.ParseJsonResponse(response.Content);
                user = new User(j);
            }
            else {
                user = new User(true, response.ReasonPhrase);
            }
            return user;
        }

        /// <summary>
        /// Returns a Paging object of the current users public and private playlists
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<Paging<ISpotifyObject>> GetCurrentUserPlaylists(string accessToken) {
            int limit = 50;
            int offset = 0;
            string req = string.Format(api_GetPlaylists, limit, offset);
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(req, accessToken);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                Paging<ISpotifyObject> paging = Paging<Playlist>.MakePlaylistPaging(token);
                return paging;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// Creates a playlist with the given playlist name
        /// </summary>
        /// <param name="playlistName"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<Playlist> CreatePlaylist(string playlistName, bool isPublic, bool isCollaborative, string description, string accessToken) {
            string req = $"https://api.spotify.com/v1/users/{Id}/playlists";
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(req, accessToken, HttpMethod.Post);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            Dictionary<string, object> keys = new Dictionary<string, object>() {
                {"name", playlistName},
                {"public", isPublic},
                {"collaborative", isCollaborative},
                {"description", description}
            };
            string postObject = JObject.FromObject(keys).ToString();
            message.Content = new StringContent(postObject);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                JObject jobj = JObject.Parse(content);
                return new Playlist(jobj);
            }
            else {
                return new Playlist(true, response.ReasonPhrase);
            }
        }

    }

    public class Album : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetAlbum = baseUrl + "/v1/albums/{0}";
        private const string api_GetAlbums = baseUrl + "/v1/albums?ids={0}";
        private const string api_GetAlbumsTracks = baseUrl + "/v1/albums/{0}/tracks";

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public Album(JToken token) {

        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Album() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Album(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }
    }

    public class AudioAnalysis : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetAudioAnalysis = baseUrl + "/v1/audio-analysis/{0}";
    }

    public class AudioFeatures : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetAudioFeature = baseUrl + "/v1/audio-features/{0}";
        private const string api_GetAudioFeatures = baseUrl + "/v1/audio-features?ids={0}";

        public int Danceability { get; } = 0;
    }

    public class Browse : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetFeaturedPlaylsits = baseUrl + "/v1/browse/featured-playlists";
        private const string api_GetNewReleases = baseUrl + "/v1/browse/new-releases";
        private const string api_GetCategories = baseUrl + "/v1/browse/categories";
        private const string api_GetCategory = baseUrl + "/v1/browse/categories/{0}";
        private const string api_GetCategoryPlaylists = baseUrl + "/vi/browse/categories/{0}/playlists";
    }

    public class Recommendation : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetRecommendations = baseUrl + "/v1/recommendations";
    }

    public class Search : SpotifyObjectModel, ISpotifyObject {
        private const string api_SearchAlbum = baseUrl + "/v1/search?type=album";
        private const string api_SearchArtist = baseUrl + "/v1/search?type=artist";
        private const string api_SearchTrack = baseUrl + "/v1/search?type=track";
        private const string api_SearchPlaylist = baseUrl + "/v1/search?type=playlist";
    }

    public class Playback : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetAvailableDevices = baseUrl + "/v1/me/player/devices";
        private const string api_GetPlayer = baseUrl + "/v1/me/player";
        private const string api_CurrentlyPlaying = baseUrl + "/v1/me/player/currently-playing";
        private const string api_StopStart = baseUrl + "/v1/me/player/play";
        private const string api_Pause = baseUrl + "/v1/me/player/pause";
        private const string api_Next = baseUrl + "/v1/me/player/next";
        private const string api_Previous = baseUrl + "/v1/me/player/previous";
        private const string api_Seek = baseUrl + "v1/me/player/seek";
        private const string api_SetRepeat = baseUrl + "/v1/me/player/repeat";
        private const string api_SetVolume = baseUrl + "/v1/me/player/volume";
        private const string api_SetShuffle = baseUrl + "/v1/me/player/shuffle";
    }

    public class Category : SpotifyObjectModel, ISpotifyObject {

        public Category(JToken token) {

        }
    }
}
