using System;
using System.Collections.Generic;
using System.Text;
using DotNetStandardSpotifyWebApi.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;
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
        private const string api_GetRecentlyPlayedTracks = baseUrl + "v1/me/player/recently-played";

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
        public ExternalUrl[] External_urls { get; } = new ExternalUrl[0];

        /// <summary>
        /// A link to the Web API endpoint for this user.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The Spotify user ID for the user. 
        /// </summary>
        public string Id { get; } = string.Empty;

        //TODO - Images
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
                ExternalUrl.FromToken(token.Value<JObject>("external_urls"));
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

        //TODO 
        //Gets the current users public and private playlists
        public async Task<List<Playlist>> GetPlaylists(string accessToken) {
            await Task.Delay(1); //TODO remove me
            return new List<Playlist>();
        }

    }

    public class Album : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetAlbum = baseUrl + "/v1/albums/{0}";
        private const string api_GetAlbums = baseUrl + "/v1/albums?ids={0}";
        private const string api_GetAlbumsTracks = baseUrl + "/v1/albums/{0}/tracks";

        public Album(JToken token) {

        }
    }

    public class Artist : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetArtist = baseUrl + "/v1/artists/{0}";
        private const string api_GetArtists = baseUrl + "/v1/artists?ids={0}";
        private const string api_GetArtistsAlbums = baseUrl + "/v1/artists/{0}/albums";
        private const string api_GetArtistsTopTracks = baseUrl + "/v1/artists/{0}/top-tracks";
        private const string api_GetRelatedArtists = baseUrl + "/v1/artists/{0}/related-artists";

        public Artist(JToken token) {

        }
    }

    public class Track : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetTrack = baseUrl + "/v1/tracks/{0}";
        private const string api_GetTracks = baseUrl + "/v1/tracks?ids={0}";

        public Track(JToken token) {

        }
    }

    public class AudioAnalysis : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetAudioAnalysis = baseUrl + "/v1/audio-analysis/{0}";
    }

    public class AudioFeatures : SpotifyObjectModel, ISpotifyObject {
        private const string api_GetAudioFeature = baseUrl + "/v1/audio-features/{0}";
        private const string api_GetAudioFeatures = baseUrl + "/v1/audio-features?ids={0}";
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

    /// <summary>
    /// Object representing both the Full and Simplified Playlist Objects
    /// https://developer.spotify.com/web-api/object-model/#playlist-object-full
    /// https://developer.spotify.com/web-api/object-model/#playlist-object-simplified
    /// </summary>
    public class Playlist : SpotifyObjectModel, ISpotifyObject {

        /// <summary>
        /// URL locations of the APIs available for getting Playlist objects
        /// </summary>
        private const string api_GetPlaylist = baseUrl + "/v1/users/{0}/playlists/{1}";
        private const string api_GetPlaylistsTracks = baseUrl + "/v1/users/{0}/playlists/{1}/tracks";
        private const string api_CreatePlaylist = baseUrl + "/v1/users/{0}/playlists";
        private const string api_GetPublicPlaylists = baseUrl + "/v1/users/{0}/playlists?limit={1}&offset={2}";
        private const string api_CheckUserFollowsPlaylist = baseUrl + "/v1/users/{0}/playlists/{1}/followers/contains";
        private const string api_FollowPlaylist = baseUrl + "/v1/users/{0}/playlists/{1}/followers";

        /// <summary>
        /// true if the owner allows other users to modify the playlist. 
        /// </summary>
        public bool Collaborative { get; } = false;

        /// <summary>
        /// The playlist description. Only returned for modified, verified playlists, otherwise empty.
        /// </summary>
        public string Description { get; } = string.Empty;

        /// <summary>
        /// Known external URLs for this playlist.
        /// </summary>
        public ExternalUrl[] External_urls { get; } = new ExternalUrl[0];

        /// <summary>
        /// Information about the followers of the playlist. 
        /// </summary>
        public Followers Followers { get; } = null;

        /// <summary>
        /// A link to the Web API endpoint providing full details of the playlist.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The Spotify ID for the playlist. 
        /// </summary>
        public string Id { get; } = string.Empty;

        /// <summary>
        /// Images for the playlist. The array may be empty or contain up to three images. The images are returned by size in descending order.
        /// See <a href="https://developer.spotify.com/web-api/working-with-playlists">working with playlists</a> for more information.
        /// Note: If returned, the source URL for the image (url) is temporary and will expire in less than a day.
        /// </summary>
        public Image[] Images { get; } = new Image[0];

        /// <summary>
        /// The name of the playlist.
        /// </summary>
        public string Name { get; } = string.Empty;

        /// <summary>
        /// The user who owns the playlist
        /// </summary>
        public User Owner { get; } = null;

        /// <summary>
        /// The playlist's public/private status: true the playlist is public, false the playlist is private, null the playlist status is not relevant. 
        /// For more about public/private status, see <a href="https://developer.spotify.com/web-api/working-with-playlists">working with playlists</a>
        /// </summary>
        public bool Public { get; } = true;

        /// <summary>
        /// The version identifier for the current playlist. Can be supplied in other requests to target a specific playlist version
        /// </summary>
        public string Snapshot_Id { get; } = string.Empty;

        /// <summary>
        /// A collection containing a link (href) to the Web API endpoint where full details of the playlist's tracks can be retrieved, along with the total number of tracks in the playlist.
        /// </summary>
        public Track[] Tracks { get; } = new Track[0];

        /// <summary>
        /// The object type: "playlist"
        /// </summary>
        public string Type { get; } = "playlist";

        /// <summary>
        /// The Spotify URI for the playlist.
        /// </summary>
        public string Uri { get; } = string.Empty;

        /// <summary>
        /// The total number of songs in this playlist.
        /// Derived from either the length of the tracks[] (for full Playlist objects)
        /// or from the "Track" psuedo-object containing a Total field (for simplified Playlist objects)
        /// </summary>
        public int Total { get; } = 0;

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public Playlist(JToken token) {
            /* Simple fields */
            Collaborative = token.Value<bool?>("collaborative ") ?? false;
            Description = token.Value<string>("description ") ?? string.Empty;
            Href = token.Value<string>("href") ?? string.Empty;
            Id = token.Value<string>("id") ?? string.Empty;
            Name = token.Value<string>("name") ?? string.Empty;
            Public = token.Value<bool?>("public") ?? true;
            Snapshot_Id = token.Value<string>("snapshot_id") ?? string.Empty;
            Uri = token.Value<string>("uri") ?? string.Empty;

            /* External URLS handling */
            JObject exturls = token.Value<JObject>("external_urls");
            if (exturls != null) {
                ExternalUrl.FromToken(exturls);
            }

            /* Follower Handling */
            JObject follower = token.Value<JObject>("followers");
            if (follower != null) {
                Followers = new Followers(follower);
            }

            /* Image Handling */
            JArray images = token.Value<JArray>("images");
            if(images != null) {
                List<Image> ims = new List<Image>();
                foreach(JObject jobj in images.Values<JObject>()) {
                    Image i = new Image(jobj);
                    ims.Add(i);
                }
                Images = ims.ToArray();
            }

            /* Owner Handling */
            JToken owner = token.Value<JToken>("owner");
            if(owner != null) {
                Owner = new User(owner);
            }

            /* Track Handling */
            JObject tracks = token.Value<JObject>("tracks");
            Total = tracks.Value<int?>("total") ?? -1;
            IEnumerable<JToken> trackValues = tracks.Values().ToList();
            // >2 properties means its really full of tracks
            if(tracks.Values().Count() > 2) {
                //TODO handle tracks
                //Paging a bitch, yo
            }
        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Playlist(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Playlist() {

        }

        /// <summary>
        /// Gets a playlist associated with the given userId and playlistId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="playlistId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static async Task<Playlist> GetPlaylist(string userId, string playlistId, string accessToken) {
            string req = string.Format(api_GetPlaylist, userId, playlistId);
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(req, accessToken);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                return new Playlist(token);
            }
            else {
                return new Playlist(true, response.ReasonPhrase);
            }
        }


        /// <summary>
        /// Returns a Paging object of Playlists. Paging object conatains the first 50 playlists. 
        /// The remaining playlists are available by using the paging's GetNext() method
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static async Task<Paging<ISpotifyObject>> GetPublicPlaylists(string userId, string accessToken) {
            int limit = 50;
            int offset = 0;
            string req = string.Format(api_GetPublicPlaylists, userId, limit, offset);
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(req, accessToken);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                Paging<ISpotifyObject> paging = Paging<Playlist>.MakePlaylistPaging(token);;
                return paging;
            }
            else {
                return null;
            }
        }


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

    /// <summary>
    /// Object containing an image as seen on Spotify
    /// For profile pics, album covers, playlist images, etc
    /// </summary>
    public class Image : SpotifyObjectModel, ISpotifyObject {
        public int Height { get; } = 0;
        public int Width { get; } = 0;
        public string Url { get; } = string.Empty;

        /// <summary>
        /// Empty constreuctor
        /// </summary>
        public Image() {

        }

        /// <summary>
        /// Error constructor
        /// </summary>
        /// <param name="wasError"></param>
        /// <param name="errorMessage"></param>
        public Image(bool wasError, string errorMessage) {
            this.WasError = wasError;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Value constructor
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="url"></param>
        public Image(int height, int width, string url) {
            this.Height = height;
            this.Width = width;
            this.Url = url;
        }

        /// <summary>
        /// JToken constructor
        /// </summary>
        /// <param name="token"></param>
        public Image(JToken token) {
            this.Height = token.Value<int?>("height") ?? 0;
            this.Width = token.Value<int?>("width") ?? 0;
            this.Url = token.Value<string>("url") ?? string.Empty;
        }
    }

    /// <summary>
    /// The offset-based paging object is a container for a set of objects.
    /// It contains a key called items (whose value is an array of the requested objects) 
    /// along with other keys like previous, next and limit that can be useful in future calls.
    /// </summary>
    /// <typeparam name="T">The type of SpotifyObeject that the paging represents</typeparam>
    public class Paging<T> : SpotifyObjectModel, ISpotifyObject where T : ISpotifyObject {

        /// <summary>
        /// A link to the Web API endpoint returning the full result of the request.
        /// </summary>
        public string Href { get; } = string.Empty;

        /// <summary>
        /// The requested data.
        /// </summary>
        public IReadOnlyList<T> Items { get; private set; } = new List<T>();

        /// <summary>
        /// The maximum number of items in the response (as set in the query or by default).
        /// </summary>
        public int Limit { get; } = 50;

        /// <summary>
        /// URL to the next page of items. (empty if none) 
        /// </summary>
        public string Next { get; } = string.Empty;

        /// <summary>
        /// URL to the previous page of items. (empty if none) 
        /// </summary>
        public string Previous { get; } = string.Empty;

        /// <summary>
        /// The offset of the items returned (as set in the query or by default).
        /// </summary>
        public int Offset { get; } = 0;

        /// <summary>
        /// The total number of items available to return. 
        /// </summary>
        public int Total { get; } = 0;


        private Paging(JToken token) {
            Href = token.Value<string>("href") ?? string.Empty;
            Total = token.Value<int?>("total") ?? 0;
            Limit = token.Value<int?>("limit") ?? 0;
            Offset = token.Value<int?>("offset") ?? 0;
            Next = token.Value<string>("next") ?? string.Empty;
            Previous = token.Value<string>("previous") ?? string.Empty;
        }


        /// <summary>
        /// Returns a new paging object with the next {limit} number of items
        /// Throws an IndexOutOfRangeException if there are no more items
        /// </summary>
        /// <returns></returns>
        public async Task<Paging<ISpotifyObject>> GetNext(string accessToken) {
            /* Offset is past the total - therefore there's nothing left to get */
            if(string.IsNullOrWhiteSpace(Next) || Offset >= Total) {
                throw new IndexOutOfRangeException($"Offset({Offset}) greater than Total number of Items({Total}). There are no more items left to get");
            }
            else { /* There are items left to get, so return a new paging object with that offset*/
                HttpRequestMessage message = WebRequestHelpers.SetupRequest(this.Next, accessToken);
                HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
                if (response.IsSuccessStatusCode) {
                    JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                    Paging<ISpotifyObject> paging = Paging<ISpotifyObject>.MakePlaylistPaging(token);
                    return paging;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns a new paging object with the previous {limit} number of items
        /// Throws an IndexOutOfRangeException if there are no more items
        /// </summary>
        /// <returns></returns>
        public async Task<Paging<ISpotifyObject>> GetPrevious(string accessToken) {
            if(string.IsNullOrWhiteSpace(Previous) || Offset <= 0) {
                throw new IndexOutOfRangeException($"Offset({Offset}) already at or below 0. There are no more items left to get");
            }
            else { /* There are items left to get, so return a new paging object with that offset*/
                HttpRequestMessage message = WebRequestHelpers.SetupRequest(this.Previous, accessToken);
                HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
                if (response.IsSuccessStatusCode) {
                    JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                    Paging<ISpotifyObject> paging = Paging<Playlist>.MakePlaylistPaging(token);
                    return paging;
                }
            }
            return null;

        }

        public static Paging<ISpotifyObject> MakePlaylistPaging(JToken token) {
            Paging<ISpotifyObject> page = new Paging<ISpotifyObject>(token);
            List<Playlist> lst = new List<Playlist>();
            JArray jarr = token.Value<JArray>("items");
            if(jarr != null) {
                foreach(JObject jobj in jarr) {
                    Playlist item = new Playlist(jobj);
                    lst.Add(item);
                }
                page.Items = lst;
            }
            return page;
        }

        public static Paging<Artist> MakeArtistPaging(JToken token) {
            Paging<Artist> page = new Paging<Artist>(token);
            List<Artist> lst = new List<Artist>();
            JArray jarr = token.Value<JArray>("items");
            if (jarr != null) {
                foreach (JObject jobj in jarr) {
                    Artist item = new Artist(jobj);
                    lst.Add(item);
                }
                page.Items = lst;
            }

            return page;
        }

        public static Paging<Album> MakeAlbumPaging(JToken token) {
            Paging<Album> page = new Paging<Album>(token);
            List<Album> lst = new List<Album>();
            JArray jarr = token.Value<JArray>("items");
            if (jarr != null) {
                foreach (JObject jobj in jarr) {
                    Album item = new Album(jobj);
                    lst.Add(item);
                }
                page.Items = lst;
            }

            return page;
        }

        public static Paging<Track> MakeTrackPaging(JToken token) {
            Paging<Track> page = new Paging<Track>(token);
            List<Track> lst = new List<Track>();
            JArray jarr = token.Value<JArray>("items");
            if (jarr != null) {
                foreach (JObject jobj in jarr) {
                    Track item = new Track(jobj);
                    lst.Add(item);
                }
                page.Items = lst;
            }

            return page;
        }

        public static Paging<Category> MakeCategoryPaging(JToken token) {
            Paging<Category> page = new Paging<Category>(token);
            List<Category> lst = new List<Category>();
            JArray jarr = token.Value<JArray>("items");
            if (jarr != null) {
                foreach (JObject jobj in jarr) {
                    Category item = new Category(jobj);
                    lst.Add(item);
                }
                page.Items = lst;
            }
            return page;
        }
    }

    public class Category : SpotifyObjectModel, ISpotifyObject {

        public Category(JToken token) {

        }
    }
}
