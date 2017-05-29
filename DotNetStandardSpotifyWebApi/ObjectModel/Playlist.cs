using System.Collections.Generic;
using DotNetStandardSpotifyWebApi.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using System.Diagnostics;

namespace DotNetStandardSpotifyWebApi.ObjectModel {
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
        private const string api_GetPlaylistsTracks = baseUrl + "/v1/users/{0}/playlists/{1}/tracks?limit={2}&offset={3}";
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
        public External_Url[] External_urls { get; } = new External_Url[0];

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
                External_Url.FromJObject(exturls);
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
        /// <param name="user_id">Spotify Id of playlist owner</param>
        /// <param name="playlist_id">Playlist id</param>
        /// <param name="accessToken">OAuth access token</param>
        /// <returns></returns>
        public static async Task<Playlist> GetPlaylist(string user_id, string playlist_id, string accessToken) {
            string req = string.Format(api_GetPlaylist, user_id, playlist_id);
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
        /// <param name="user_id">Spotify Id of playlist owner</param>
        /// <param name="accessToken">OAuth access token</param>
        /// <returns></returns>
        public static async Task<Paging<ISpotifyObject>> GetPublicPlaylists(string user_id, string accessToken) {
            int limit = 50;
            int offset = 0;
            string req = string.Format(api_GetPublicPlaylists, user_id, limit, offset);
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

        /// <summary>
        /// Returns a paging object of tracks from a specified playlist
        /// </summary>
        /// <param name="user_id">Spotify Id of Playlist owner</param>
        /// <param name="playlist_id">Playist Id</param>
        /// <param name="accessToken">OAuth access token</param>
        /// <returns></returns>
        public static async Task<Paging<ISpotifyObject>> GetTracks(string user_id, string playlist_id, string accessToken) {
            int limit = 100;
            int offset = 0;
            string req = string.Format(api_GetPlaylistsTracks, user_id, playlist_id, limit, offset);
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(req, accessToken);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                JToken token = await WebRequestHelpers.ParseJsonResponse(response.Content);
                Paging<ISpotifyObject> page = Paging<ISpotifyObject>.MakeTrackPaging(token);
                return page;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// Adds the given TrackURIS to the specified playlist
        /// Returns a snapshot id of the newly modified playlist
        /// </summary>
        /// <param name="userId">Spotify ID of playlist owner</param>
        /// <param name="playlistId">Playlist ID</param>
        /// <param name="trackUris">Spotify URIs for the desired tracks</param>
        /// <param name="position">Optional. The position to insert the tracks, a zero-based index. For example, to insert the tracks in the first position: position=0; to insert the tracks in the third position: position=2. If omitted, the tracks will be appended to the playlist. Tracks are added in the order they are listed in the query string or request body.</param>
        /// <param name="accessToken">OAuth access token</param>
        /// <returns>snapshot id of modified playlist</returns>
        public static async Task<string> AddTracks(string user_id, string playlist_id, IEnumerable<string> trackUris, int postition, string accessToken) {
            string req = $"https://api.spotify.com/v1/users/{user_id}/playlists/{playlist_id}/tracks";
            HttpRequestMessage message = WebRequestHelpers.SetupRequest(req, accessToken, HttpMethod.Post);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            Dictionary<string, object> keys = new Dictionary<string, object>() {
                {"uris", trackUris},
                {"position", postition}
            };
            string postObject = JObject.FromObject(keys).ToString();
            message.Content = new StringContent(postObject);
            HttpResponseMessage response = await WebRequestHelpers.Client.SendAsync(message);
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                JObject jobj = JObject.Parse(content);
                return jobj.Value<string>("snapshot_id");
            }
            else {
                //Error value
                return "";
            }
        }

    }
}
