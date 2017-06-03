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
        public Dictionary<string, string> External_Urls { get; } = new Dictionary<string, string>();

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
            if (exturls != null) {
                foreach (JProperty x in exturls.Properties()) {
                    External_Urls.Add(x.Name, x.Value<JToken>().ToString());
                }
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
        public async Task<Paging<Playlist>> GetCurrentUserPlaylists(string accessToken) {
            int limit = 50;
            int offset = 0;
            string req = string.Format(api_GetPlaylists, limit, offset);
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(req, accessToken);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                Paging<Playlist> paging = new Paging<Playlist>(token);
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

    public class AudioAnalysis : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetAudioAnalysis = baseUrl + "/v1/audio-analysis/{0}";


        public AudioAnalysis() {

        }

        public AudioAnalysis(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }

        public AudioAnalysis(JToken token) {

        }
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

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Playback() {

        }

        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Playback(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public Playback(JToken token) {

        }
    }

    public class Device : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// The device ID. This may be empty.
        /// </summary>
        string Id { get; } = string.Empty;

        /// <summary>
        /// If this device is the currently active device.
        /// </summary>
        bool Is_Active { get; } = false;

        /// <summary>
        /// Whether controlling this device is restricted.
        /// At present if this is "true" then no Web API commands will be accepted by this device.
        /// </summary>
        bool Is_Restricted { get; } = true;

        /// <summary>
        /// The name of the device.
        /// </summary>
        string Name { get; } = string.Empty;

        /// <summary>
        /// Device type, such as "Computer", "Smartphone" or "Speaker".
        /// </summary>
        string Type { get; } = string.Empty;

        /// <summary>
        /// The current volume in percent. This may be null.
        /// </summary>
        int? Volume_Percent { get; } = 0;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Device() {

        }

        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Device(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public Device(JToken token) {
            Id = token.Value<string>("id") ?? string.Empty;
            Is_Active = token.Value<bool?>("is_active") ?? false;
            Is_Restricted = token.Value<bool?>("is_restricted") ?? true;
            Name = token.Value<string>("name") ?? string.Empty;
            Type = token.Value<string>("type") ?? string.Empty;
        }
    }

    public class CurrentlyPlayingContext : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// The device that is currently active
        /// </summary>
        public Device Device { get; } = new Device();

        /// <summary>
        /// off, track, context
        /// </summary>
        public string Repeat_State { get; } = "off";

        /// <summary>
        /// If shuffle is on or off
        /// </summary>
        public bool Shuffle_State { get; } = false;

        /// <summary>
        /// A Context Object. Can be null.
        /// </summary>
        public Context Context { get; } = new Context();

        /// <summary>
        /// Timestamp when data was fetched
        /// </summary>
        public long Timestamp { get; } = 0;

        /// <summary>
        /// Progress into the currently playing track. Can be null.
        /// </summary>
        public long Progress_Ms { get; } = 0;

        /// <summary>
        /// If something is currently playing.
        /// </summary>
        public bool Is_Playing { get; } = false;

        /// <summary>
        /// The currently playing track. Can be null.
        /// </summary>
        public Track Item { get; } = new Track();


        /// <summary>
        /// Empty Constructor
        /// </summary>
        public CurrentlyPlayingContext() {

        }

        /// <summary>
        /// Error Constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public CurrentlyPlayingContext(bool wasError, string errorMessage) {
            WasError = wasError;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// JToken Constructor
        /// </summary>
        /// <param name="token"></param>
        public CurrentlyPlayingContext(JToken token) {
            /* Simple Fields */
            Is_Playing = token.Value<bool?>("is_playing") ?? false;
            Progress_Ms = token.Value<long?>("progress_ms") ?? 0;
            Timestamp = token.Value<long?>("timestamp") ?? 0;
            Shuffle_State = token.Value<bool?>("shuffle_state") ?? false;
            Repeat_State = token.Value<string>("repeat_state") ?? "off";

            JObject contextObject = token.Value<JObject>("context");
            if(contextObject != null) {
                Context = new Context(contextObject);
            }

            JObject trackObject = token.Value<JObject>("item");
            if(trackObject != null) {
                Item = new Track(trackObject);
            }
        }
    }

}
