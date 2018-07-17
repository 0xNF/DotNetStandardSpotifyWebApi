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
    public class Playlist : SpotifyObjectModel, ISpotifyObject, ISimpleSpotifyObject, IFullSpotifyObject {

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
        public Dictionary<string, string> External_Urls { get; } = new Dictionary<string, string>();

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
        public Paging<PlaylistTrack> Tracks { get; } = new Paging<PlaylistTrack>(true, "Default, not populated yet");

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
            Collaborative = token.Value<bool?>("collaborative") ?? false;
            Description = token.Value<string>("description") ?? string.Empty;
            Href = token.Value<string>("href") ?? string.Empty;
            Id = token.Value<string>("id") ?? string.Empty;
            Name = token.Value<string>("name") ?? string.Empty;
            Public = token.Value<bool?>("public") ?? true;
            Snapshot_Id = token.Value<string>("snapshot_id") ?? string.Empty;
            Uri = token.Value<string>("uri") ?? string.Empty;

            /* External URLS handling */
            JObject exturls = token.Value<JObject>("external_urls");
            if (exturls != null) {
                foreach (JProperty x in exturls.Properties()) {
                    External_Urls.Add(x.Name, x.Value<JToken>().ToString());
                }
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
            if(owner != null && owner.HasValues) {
                Owner = new User(owner);
            }
            

            /* Track Handling */
            JObject tracks = token.Value<JObject>("tracks");
            if (tracks != null) {
                Total = tracks?.Value<int?>("total") ?? -1;
                IEnumerable<JToken> trackValues = tracks?.Values().ToList();
                // >2 properties means its really full of tracks
                if (tracks.Values().Count() > 2) {
                    //TODO handle tracks
                    //Paging a bitch, yo
                }
            }
        }

        /// <summary>
        /// Fields Constructor
        /// </summary>
        public Playlist(string playlistId, string playlistName, string description, User owner, Followers followers, string snapshotId,  bool isPublic, bool isCollaborative, string href, string uri, Dictionary<string, string> externalUrls, IEnumerable<Image> images, Paging<PlaylistTrack> tracks) {
            this.Collaborative = isCollaborative;
            this.Description = description;
            this.External_Urls = externalUrls;
            this.Followers = followers;
            this.Href = href;
            this.Id = playlistId;
            this.Images = images.ToArray();
            this.Name = playlistName;
            this.Owner = owner;
            this.Public = isPublic;
            this.Snapshot_Id = snapshotId;
            this.Total = tracks?.Total ?? 0;
            this.Uri = uri;
            this.Tracks = tracks;
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

        public JObject ToSimpleJson() {
            JArray jimages = new JArray();
            foreach(Image i in this.Images) {
                jimages.Add(i.ToJson());
            }

            Dictionary<string, object> keys = new Dictionary<string, object>() {
                { "collaborative", this.Collaborative },
                { "external_urls", JObject.FromObject(this.External_Urls) },
                { "href", this.Href },
                { "id", this.Id },
                { "images", jimages },
                { "name", this.Name },
                { "owner", this.Owner.ToPublicJson() },
                { "public", this.Public },
                { "snapshot_id", this.Snapshot_Id },
                { "tracks", new Followers(this.Href+"/tracks", this.Tracks.Total).ToJson() }, // Because a Followers object is structurally identical to this simple track href object
                { "type", this.Type },
                { "uri", this.Uri }
            };
            return JObject.FromObject(keys);
        }

        public JObject ToFullJson() {
            JObject simple = this.ToSimpleJson();
            simple.Add("description", this.Description);
            simple.Add("followers", this.Followers.ToJson());
            simple.Remove("tracks");
            simple.Add("tracks", this.Tracks.ToJson());

            return simple;
        }

        public JToken ToJson() {
            return this.ToFullJson();
        }

    }
}
